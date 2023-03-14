using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Api.Common;
using Boilerplate.Application.Features.Auth.UpdateEmail;
using Boilerplate.Domain.PermissionsCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StatusGeneric;
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

    public AuthpUsersController(IAuthUsersAdminService authUsersAdmin, IMediator mediator)
    {
        _authUsersAdmin = authUsersAdmin;
        _mediator = mediator;
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
        var status = await _authUsersAdmin.UpdateUserAsync(change.UserId,
            change.Email, change.UserName, change.RoleNames, change.TenantName);

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
    public async Task<IStatusGeneric> SyncUsers(IEnumerable<SyncAuthUserWithChange> data)
    {
        return await _authUsersAdmin.ApplySyncChangesAsync(data);
    }

    // GET: AuthUsersController/Delete/5
    [HasPermission(DefaultPermissions.UserRemove)]
    [HttpGet]
    [Route("delete")]
    public async Task<AuthUserDisplay> Delete(string userId)
    {
        var status = await _authUsersAdmin.FindAuthUserByUserIdAsync(userId);
        return AuthUserDisplay.DisplayUserInfo(status.Result);
    }

    


    [HttpGet]
    [Route("errordisplay")]
    public ActionResult ErrorDisplay(string errorMessage)
    {
        return Ok((object)errorMessage);
    }
}
