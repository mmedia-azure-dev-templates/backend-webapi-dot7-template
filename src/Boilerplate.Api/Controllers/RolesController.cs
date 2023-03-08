using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.Classes.SupportTypes;
using AuthPermissions.BaseCode.PermissionsCode;
using Boilerplate.Api.Common;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;
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
public class RolesController : ControllerBase
{
    private readonly IAuthRolesAdminService _authRolesAdmin;

    public RolesController(IAuthRolesAdminService authRolesAdmin)
    {
        _authRolesAdmin = authRolesAdmin;
    }

    [HasPermission(DefaultPermissions.RoleRead)]
    [HttpGet]
    [Route("list-roles")]
    public async Task<ActionResult<List<RoleWithPermissionNamesDto>>> ListRoles()
    {
        var userId = User.GetUserIdFromUser();
        List<RoleWithPermissionNamesDto> permissionDisplay = await
            _authRolesAdmin.QueryRoleToPermissions(userId)
                .OrderBy(x => x.RoleType)
                .ToListAsync();

        return permissionDisplay;
    }

    [HasPermission(DefaultPermissions.PermissionRead)]
    [HttpGet]
    [Route("list-permissions")]
    public ActionResult<List<PermissionDisplay>> ListPermissions()
    {
        List<PermissionDisplay> permissionDisplays = _authRolesAdmin.GetPermissionDisplay(false);
        return permissionDisplays;
    }

    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpGet]
    [Route("edit")]
    public async Task<ActionResult<RoleCreateUpdateDto>> Edit(string roleName)
    {
        var userId = User.GetUserIdFromUser();
        var role = await _authRolesAdmin.QueryRoleToPermissions(userId).SingleOrDefaultAsync(x => x.RoleName == roleName);
        var permissionsDisplay = _authRolesAdmin.GetPermissionDisplay(false);
        RoleCreateUpdateDto roleCreateUpdate = RoleCreateUpdateDto.SetupForCreateUpdate(role.RoleName, role.Description, role.PermissionNames, permissionsDisplay, role.RoleType);
        return roleCreateUpdate;
    }

    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpPost]
    [Route("edit")]
    public async Task<IStatusGeneric> Edit(RoleCreateUpdateDto input)
    {
        return await _authRolesAdmin.UpdateRoleToPermissionsAsync(input.RoleName, input.GetSelectedPermissionNames(), input.Description, input.RoleType);
    }

    [HasPermission(DefaultPermissions.RoleChange)]
    [HttpPost]
    [Route("create")]
    public async Task<CustomStatusGeneric> Create(RoleCreateUpdateDto input)
    {
        IStatusGeneric statusGeneric = await _authRolesAdmin.CreateRoleToPermissionsAsync(input.RoleName, input.GetSelectedPermissionNames(), input.Description, input.RoleType);
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid? statusGeneric.Message : errors;
        return customStatusGeneric;
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