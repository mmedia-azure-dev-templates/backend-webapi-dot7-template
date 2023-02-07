using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Api.Common;
using Boilerplate.Domain.PermissionsCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RolesController : ControllerBase
{
    private readonly IAuthRolesAdminService _authRolesAdmin;

    public RolesController(IAuthRolesAdminService authRolesAdmin)
    {
        _authRolesAdmin = authRolesAdmin;
    }

    [HasPermission(DefaultPermissions.RoleRead)]
    [HttpGet]
    public async Task<IActionResult> Index(string message)
    {
        var userId = User.GetUserIdFromUser();
        var permissionDisplay = await
            _authRolesAdmin.QueryRoleToPermissions(userId)
                .OrderBy(x => x.RoleType)
                .ToListAsync();
        return Ok(permissionDisplay);
    }

    [HasPermission(DefaultPermissions.PermissionRead)]
    [HttpGet]
    [Route("list-permissions")]
    public IActionResult ListPermissions()
    {
        var permissionDisplay = _authRolesAdmin.GetPermissionDisplay(false);

        return Ok(permissionDisplay);
    }

    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpPost]
    [Route("edit")]
    public async Task<IActionResult> Edit(RoleCreateUpdateDto input)
    {
        var status = await _authRolesAdmin
            .UpdateRoleToPermissionsAsync(input.RoleName, input.GetSelectedPermissionNames(), input.Description, input.RoleType);
        return Ok(status);
    }

    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(RoleCreateUpdateDto input)
    {
        var status = await _authRolesAdmin
            .CreateRoleToPermissionsAsync(input.RoleName, input.GetSelectedPermissionNames(), input.Description, input.RoleType);
        return Ok(status);
        //if (status.HasErrors)
        //    return RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() });

        //return RedirectToAction(nameof(Index), new { message = status.Message });
    }
    
    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("delete")]
    public async Task<IActionResult> Delete(RoleDeleteConfirmDto input)
    {
        var status = await _authRolesAdmin.DeleteRoleAsync(input.RoleName, input.ConfirmDelete?.Trim() == input.RoleName);
        return Ok(status);
        //if (status.HasErrors)
        //    return RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() });

        //return RedirectToAction(nameof(Index), new { message = status.Message });
    }
}
