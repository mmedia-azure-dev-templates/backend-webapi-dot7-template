﻿using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.SupportCode.AddUsersServices;
using Boilerplate.Api.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.PermissionsCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantAdminController : Controller
{
    private readonly IAuthUsersAdminService _authUsersAdmin;

    public TenantAdminController(IAuthUsersAdminService authUsersAdmin)
    {
        _authUsersAdmin = authUsersAdmin;
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
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid ? statusGeneric.Message : errors;
        return customStatusGeneric;
    }
    /*
    public ActionResult ErrorDisplay(string errorMessage)
    {
        return View((object)errorMessage);
    }

    //-------------------------------------------------------

    //Thanks to https://stackoverflow.com/questions/30755827/getting-absolute-urls-using-asp-net-core
    public string AbsoluteAction(IUrlHelper url,
        string actionName,
        string controllerName,
        object routeValues = null)
    {
        string scheme = HttpContext.Request.Scheme;
        return url.Action(actionName, controllerName, routeValues, scheme);
    }
    */
}