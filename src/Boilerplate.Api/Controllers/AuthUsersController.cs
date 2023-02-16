using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Api.Common;
using Boilerplate.Domain.PermissionsCode;
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
public class AuthUsersController : ControllerBase
{
    private readonly IAuthUsersAdminService _authUsersAdmin;

    public AuthUsersController(IAuthUsersAdminService authUsersAdmin)
    {
        _authUsersAdmin = authUsersAdmin;
    }

    // List users filtered by authUser tenant
    //[HasPermission(Example3Permissions.UserRead)] !!! Turned off so that I can list all the users
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
    public async Task<ActionResult> SyncUsers()
    {
        var syncChanges = await _authUsersAdmin.SyncAndShowChangesAsync();
        return Ok(syncChanges);
    }

    [HttpPost]
    [Route("authuserssync")]
    [HasPermission(DefaultPermissions.UserSync)]
    //NOTE: the input be called "data" because we are using JavaScript to send that info back
    public async Task<ActionResult> SyncUsers(IEnumerable<SyncAuthUserWithChange> data)
    {
        var status = await _authUsersAdmin.ApplySyncChangesAsync(data);
        if (status.HasErrors)
            return RedirectToAction(nameof(ErrorDisplay),
                new { errorMessage = status.GetAllErrors() });

        return Ok(new { message = status.Message });
    }

    // GET: AuthUsersController/Delete/5
    [HasPermission(DefaultPermissions.UserRemove)]
    [HttpGet]
    [Route("delete")]
    public async Task<ActionResult> Delete(string userId)
    {
        var status = await _authUsersAdmin.FindAuthUserByUserIdAsync(userId);
        if (status.HasErrors)
            return RedirectToAction(nameof(ErrorDisplay),
                new { errorMessage = status.GetAllErrors() });

        return Ok(AuthUserDisplay.DisplayUserInfo(status.Result));
    }

    // POST: AuthUsersController/Delete/5
    //[HttpPost]
    //[ValidateAntiForgeryToken]
    //[HasPermission(DefaultPermissions.UserRemove)]
    //public async Task<ActionResult> Delete(AuthIdAndChange input)
    //{
    //    var status = await _authUsersAdmin.DeleteUserAsync(input.UserId);
    //    if (status.HasErrors)
    //        return RedirectToAction(nameof(ErrorDisplay),
    //            new { errorMessage = status.GetAllErrors() });

    //    return RedirectToAction(nameof(Index), new { message = status.Message });
    //}
    [HttpGet]
    [Route("errordisplay")]
    public ActionResult ErrorDisplay(string errorMessage)
    {
        return Ok((object)errorMessage);
    }
}
