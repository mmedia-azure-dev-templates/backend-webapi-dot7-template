using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
//using AuthPermissions.AspNetCore.AccessTenantData;
//using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.PermissionsCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TenantController : ControllerBase
{
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public TenantController(IAuthTenantAdminService authTenantAdmin)
    {
        _authTenantAdmin = authTenantAdmin;
    }

    
    [HttpGet]
    [HasPermission(DefaultPermissions.TenantList)]
    [Route("tenants")]
    public async Task<List<SingleLevelTenantDto>> Tenants()
    {
        var tenantNames = await SingleLevelTenantDto.TurnIntoDisplayFormat(_authTenantAdmin.QueryTenants())
            .OrderBy(x => x.TenantName)
            .ToListAsync();
        return tenantNames;
    }

    [HttpGet]
    [Route("create")]
    [HasPermission(DefaultPermissions.TenantCreate)]
    public async Task<SingleLevelTenantDto> Create()
    {
        return new SingleLevelTenantDto { AllPossibleRoleNames = await _authTenantAdmin.GetRoleNamesForTenantsAsync() };
    }


    [HttpPost]
    [Route("create")]
    [HasPermission(DefaultPermissions.TenantCreate)]
    public async Task<Tenant> Create(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin.AddSingleTenantAsync(input.TenantName, input.TenantRolesName);
        return status.Result;
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("edit")]
    [HasPermission(DefaultPermissions.TenantUpdate)]
    public async Task<IActionResult> Edit(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin
            .UpdateTenantNameAsync(input.TenantId, input.TenantName);
        return Ok(status);
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("delete")]
    [HasPermission(DefaultPermissions.TenantDelete)]
    public async Task<IActionResult> Delete(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin.DeleteTenantAsync(input.TenantId);
        return Ok();
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
    }

    [HasPermission(DefaultPermissions.TenantAccessData)]
    [HttpGet]
    [Route("startAccess")]
    public async Task<IActionResult> StartAccess([FromServices] ILinkToTenantDataService service, int id)
    {
        var currentUser = User.GetUserIdFromUser();
        var status = await service.StartLinkingToTenantDataAsync(currentUser, id);

        return Ok(status);
    }

    [HttpGet]
    [Route("stopAccess")]
    public IActionResult StopAccess([FromServices] ILinkToTenantDataService service, bool gotoHome)
    {
        var currentUser = User.GetUserIdFromUser();
        service.StopLinkingToTenant();

        return Ok();
    }
}
