using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.EfCode;
using Boilerplate.Api.Common;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Auth.UpdateEmail;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.PermissionsCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using org.apache.zookeeper.data;
using StatusGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthpUsersController : ControllerBase
{
    private readonly IAuthUsersAdminService _authUsersAdmin;
    private readonly IMediator _mediator;
    private readonly AuthPermissionsDbContext _context;

    public AuthpUsersController(IAuthUsersAdminService authUsersAdmin, IMediator mediator, AuthPermissionsDbContext context)
    {
        _authUsersAdmin = authUsersAdmin;
        _mediator = mediator;
        _context = context;
    }

    // List users filtered by authUser tenant !!! Turned off so that I can list all the users
    [HasPermission(DefaultPermissions.UserRead)] 
    [HttpGet]
    [Route("authusers")]
    public async Task<List<AuthUserDisplay>> AuthUsers()
    {
        var authDataKey = User.GetAuthDataKeyFromUser();
        var userQuery = _authUsersAdmin.QueryAuthUsers(authDataKey);
        var usersToShow = await AuthUserDisplay.TurnIntoDisplayFormat(userQuery.OrderBy(x => x.Email)).ToListAsync();
        return usersToShow;
    }

    [HasPermission(DefaultPermissions.UserChange)]
    [HttpGet]
    [Route("edit")]
    public async Task<SetupManualUserChange> Edit(string userId)
    {
        var status = await SetupManualUserChange.PrepareForUpdateAsync(userId, _authUsersAdmin);
        return status.Result;
    }

    [HttpPost]
    [Route("edit")]
    [HasPermission(DefaultPermissions.UserChange)]
    public async Task<string> Edit(SetupManualUserChange change)
    {
        var status = await _authUsersAdmin.UpdateUserAsync(change.UserId, change.Email, change.UserName, change.RoleNames, change.TenantName);
        return status.Message;
    }

    [HasPermission(DefaultPermissions.UserSync)]
    [HttpGet]
    [Route("authuserssync")]
    public async Task<List<SyncAuthUserWithChange>> SyncUsers()
    {
        return await _authUsersAdmin.SyncAndShowChangesAsync();
    }

    [HttpPost]
    [Route("authuserssync")]
    [HasPermission(DefaultPermissions.UserSync)]
    //NOTE: the input be called "data" because we are using JavaScript to send that info back
    public async Task<CustomStatusGeneric> SyncUsers(IEnumerable<SyncAuthUserWithChange> data)
    {
        IStatusGeneric statusGeneric = await _authUsersAdmin.ApplySyncChangesAsync(data);
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid ? statusGeneric.Message : errors;
        return customStatusGeneric;
    }

    // GET: AuthUsersController/Delete/5
    [HasPermission(DefaultPermissions.UserRemove)]
    [HttpGet]
    [Route("delete")]
    public async Task<CustomStatusGeneric> Delete(UserId userId)
    {
        IStatusGeneric statusGeneric = await _authUsersAdmin.DeleteUserAsync(userId.ToString());
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid ? statusGeneric.Message : errors;
        return customStatusGeneric;
    }

    [HasPermission(DefaultPermissions.AccessAll)]
    [HttpPost]
    [Route("settenantsuperadmin")]
    public async Task<string> SetTenantSuperAdmin(int? tenantId)
    {
        var status = await _authUsersAdmin.FindAuthUserByUserIdAsync(User.Claims.GetUserIdFromClaims().ToString());

        if (status.IsValid)
        {
            var isSuperAdmin = status.Result.UserRoles.Where(x => x.RoleName == "SuperAdmin").Count();

            if (isSuperAdmin == 0)
            {
                return "No tienes el rol Super Admin :(";
            }

            //var commandText = "UPDATE authp.AuthUsers SET TenantId = @NewTenantId WHERE UserId = @MyId";

            object[] paramItems = new object[]
            {
                    new SqlParameter("@tenantId", tenantId == null ? DBNull.Value : tenantId),
                    new SqlParameter("@UserId",status.Result.UserId)
            };
            int items = _context.Database.ExecuteSqlRaw
                ("UPDATE authp.AuthUsers SET TenantId = @tenantId WHERE [UserId] = @UserId", paramItems);
            //_context.AuthUsers.FromSqlRaw("UPDATE authp.AuthUsers SET TenantId={NewTenantId} WHERE UserId={UserId}");

            return "Tenant Super Admin Actualizado Correctamente!";
        }

        return status.Message;
    }


    [HttpGet]
    [Route("errordisplay")]
    public ActionResult ErrorDisplay(string errorMessage)
    {
        return Ok((object)errorMessage);
    }
}
