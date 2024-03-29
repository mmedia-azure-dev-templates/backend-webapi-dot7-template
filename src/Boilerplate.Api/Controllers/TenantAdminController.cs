﻿using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using Boilerplate.Application.Features.TenantAdmin.VerifyInvitation;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.PermissionsCode;
using LocalizeMessagesAndErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantAdminController : Controller
{
    private readonly IAuthRolesAdminService _authRolesAdmin;
    private readonly IAuthUsersAdminService _authUsersAdmin;
    private readonly IEncryptDecryptService _encryptService;
    private readonly IAuthUsersAdminService _usersAdmin;
    private readonly IAddNewUserManager _addNewUserManager;
    private readonly IDefaultLocalizer _localizeDefault;
    private readonly IMailService _mailService;
    private readonly ISession _session;
    private readonly UserManager<ApplicationUser> _userManager;

    public TenantAdminController(IAuthRolesAdminService authRolesAdmin,IAuthUsersAdminService authUsersAdmin, IEncryptDecryptService encryptService,
        IAuthUsersAdminService usersAdmin, IAddNewUserManager addNewUserManager, IAuthPDefaultLocalizer localizeProvider, IMailService mailService,ISession session, UserManager<ApplicationUser> userManager)
    {
        _authRolesAdmin = authRolesAdmin;
        _authUsersAdmin = authUsersAdmin;
        _encryptService = encryptService;
        _usersAdmin = usersAdmin;
        _addNewUserManager = addNewUserManager;
        _localizeDefault = localizeProvider.DefaultLocalizer;
        _mailService = mailService;
        _session = session;
        _userManager = userManager;
    }

    [HasPermission(DefaultPermissions.UserRead)]
    [HttpGet]
    public async Task<IActionResult> Index(string message)
    {
        var dataKey = User.GetAuthDataKeyFromUser();
        var userQuery = _authUsersAdmin.QueryAuthUsers(dataKey);
        var usersToShow = await AuthUserDisplay.TurnIntoDisplayFormat(userQuery.OrderBy(x => x.Email)).ToListAsync();

        ViewBag.Message = message;

        return Ok();
    }
    
    [HttpPost]
    [HasPermission(DefaultPermissions.UserRolesChange)]
    [Route("edit-roles")]
    public async Task<ActionResult> EditRoles(SetupManualUserChange change)
    {
        var status = await _authUsersAdmin.UpdateUserAsync(change.UserId,
            roleNames: change.RoleNames);

        if (status.HasErrors)
            return Ok(status.GetAllErrors());
                //new { errorMessage = status.GetAllErrors() });

        return RedirectToAction(nameof(Index), new { message = status.Message });
    }


    [HasPermission(DefaultPermissions.InviteUsers)]
    [HttpGet]
    [Route("invite-user")]
    public async Task<InviteUserSetup> InviteUser([FromServices] IInviteNewUserService inviteService)
    {
        var setupInvite = new InviteUserSetup
        {
            AllRoleNames = await _authUsersAdmin.GetRoleNamesForUsersAsync(User.GetUserIdFromUser()),
            ExpirationTimesDropdown = inviteService.ListOfExpirationTimes()
        };
        var status = await _authUsersAdmin.FindAuthUserByUserIdAsync(User.GetUserIdFromUser());
        var isAdminTenant = status.Result.UserRoles.Where(x => x.RoleName == "Administrador Empresa").Count();

        if (isAdminTenant == 0)
        {
            setupInvite.AllRoleNames = setupInvite.AllRoleNames.Where(x => x != "Administrador Empresa").ToList();
        }

        return setupInvite;
    }

    [HasPermission(DefaultPermissions.InviteUsers)]
    [HttpPost]
    [Route("invite-user")]
    public async Task<CustomStatusGeneric> InviteUser([FromServices] IInviteNewUserService inviteUserServiceService, InviteUserSetup data)
    {
        var addUserData = new AddNewUserDto
        {
            Email = data.Email,
            Roles = data.RoleNames,
            TimeInviteExpires = data.InviteExpiration
        };
        var statusGeneric = await inviteUserServiceService.CreateInviteUserToJoinAsync(addUserData, User.GetUserIdFromUser());
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        if (statusGeneric.HasErrors)
        {
            customStatusGeneric.IsValid = statusGeneric.IsValid;
            customStatusGeneric.Message = errors;
            return customStatusGeneric;
        }


        var user = await _userManager.FindByIdAsync(_session.UserId.ToString());

        MailStruct mailData = new MailStruct(
                    "Nueva invitación de " + user!.FirstName + " " + user.LastName,
                    new List<string> {
                        addUserData.Email
                    },
                    "Nueva invitación de " + user.FirstName + " " + user.LastName,
                    "ConfirmationInvitationView"
                   );

        // Create MailData object
        ConfirmationInvitationMailData confirmationInvitationMailData = new ConfirmationInvitationMailData()
        {
            Name = user.FirstName + " " + user.LastName,
            Token = statusGeneric.Result
        };

        bool emailStatus = await _mailService.CreateEmailMessage(mailData, confirmationInvitationMailData, new CancellationToken());
        if (!emailStatus)
        {
            customStatusGeneric.IsValid = false;
            customStatusGeneric.Message = "Error al enviar el correo electrónico.";
            return customStatusGeneric;
        }

        object result = new
        {
            url = statusGeneric.Result
        };

        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.Message;
        customStatusGeneric.Result = result;
        return customStatusGeneric;
    }

    [HttpGet]
    [Route("verifyInvitation")]
    [AllowAnonymous]
    public VerifyInvitationResponse verifyInvitation([FromQuery] string inviteParam)
    {
        VerifyInvitationResponse verifyInvitationResponse = new VerifyInvitationResponse();
        
        try
        {
            var decrypted = _encryptService.Decrypt(Base64UrlEncoder.Decode(inviteParam));
            var newUserData = JsonSerializer.Deserialize<AddNewUserDto>(decrypted);

            if (newUserData == null)
            {
                verifyInvitationResponse.Message = "La invitación no es válida!. Póngase en contacto con la persona que envió la invitación.";
                return verifyInvitationResponse;
            }
  
            if (newUserData.TimeInviteExpires != default && newUserData.TimeInviteExpires < DateTime.UtcNow.Ticks)
            {
                verifyInvitationResponse.Message = "La invitación ha expirado!. Póngase en contacto con la persona que envió la invitación.";
                return verifyInvitationResponse;
            }

            verifyInvitationResponse.IsValid = true;
            verifyInvitationResponse.Message = "Excelente la invitación es válida!";
            verifyInvitationResponse.Email = newUserData.Email;
            return verifyInvitationResponse;
        }
        catch (Exception e)
        {
            verifyInvitationResponse.Message = e.Message;
            return verifyInvitationResponse;
        }
    }

    [HttpGet]
    [Route("confirmInvitation")]
    [AllowAnonymous]
    public VerifyInvitationResponse confirmInvitation([FromQuery] string inviteParam, string email)
    {
        VerifyInvitationResponse verifyInvitationResponse = new VerifyInvitationResponse();

        try
        {
            var normalizedEmail = email.Trim().ToLower();
            var decrypted = _encryptService.Decrypt(Base64UrlEncoder.Decode(inviteParam));
            var newUserData = JsonSerializer.Deserialize<AddNewUserDto>(decrypted);

            if(newUserData == null)
            {
                verifyInvitationResponse.Message = "La invitación no es válida!. Póngase en contacto con la persona que envió la invitación.";
                return verifyInvitationResponse;
            }

            if (newUserData.Email != normalizedEmail)
            {
                verifyInvitationResponse.Message = "Correo electrónico no coincide con la invitación.";
                return verifyInvitationResponse;
            }

            if (newUserData.TimeInviteExpires != default && newUserData.TimeInviteExpires < DateTime.UtcNow.Ticks)
            {
                verifyInvitationResponse.Message = "La invitación ha expirado!. Póngase en contacto con la persona que envió la invitación.";
                return verifyInvitationResponse;
            }

            verifyInvitationResponse.IsValid = true;
            verifyInvitationResponse.Message = "Excelente la invitación es válida!";
            verifyInvitationResponse.Email = newUserData.Email;
            return verifyInvitationResponse;
        }
        catch (Exception e)
        {
            verifyInvitationResponse.Message = e.Message;
            return verifyInvitationResponse;
        }
    }
}