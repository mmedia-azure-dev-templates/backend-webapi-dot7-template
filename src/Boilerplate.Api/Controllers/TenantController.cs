using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.AccessTenantData;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.Classes;
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

    [HttpGet]
    [HasPermission(DefaultPermissions.TenantUpdate)]
    [Route("edit")]
    public async Task<SingleLevelTenantDto> Edit(int id)
    {
        return await SingleLevelTenantDto.SetupForUpdateAsync(_authTenantAdmin, id);
    }


    [HttpPost]
    [Route("edit")]
    [HasPermission(DefaultPermissions.TenantUpdate)]
    public async Task<CustomStatusGeneric> Edit(SingleLevelTenantDto input)
    {
        IStatusGeneric statusGeneric = await _authTenantAdmin.UpdateTenantNameAsync(input.TenantId, input.TenantName);
        IStatusGeneric statusGeneric2 = await _authTenantAdmin.UpdateTenantRolesAsync(input.TenantId, input.TenantRolesName);
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        var errors2 = string.Join(" | ", statusGeneric2.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid ? statusGeneric.Message : errors + errors2;
        return customStatusGeneric;
    }

    [HttpGet]
    [HasPermission(DefaultPermissions.TenantDelete)]
    [Route("delete")]
    public async Task<CustomStatusGeneric> Delete(int id)
    {
        IStatusGeneric<Tenant> statusGeneric = await _authTenantAdmin.GetTenantViaIdAsync(id);
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        if (statusGeneric.HasErrors)
        {
            customStatusGeneric.IsValid = statusGeneric.IsValid;
            customStatusGeneric.Message = errors;
            return customStatusGeneric;
        }

        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.Message;
        customStatusGeneric.Result = statusGeneric.Result;
        return customStatusGeneric;
    }

    [HttpPost]
    [Route("delete")]
    [HasPermission(DefaultPermissions.TenantDelete)]
    public async Task<CustomStatusGeneric> Delete(SingleLevelTenantDto input)
    {
        IStatusGeneric<ITenantChangeService> statusGeneric = await _authTenantAdmin.DeleteTenantAsync(input.TenantId);
        var errors = string.Join(" | ", statusGeneric.Errors.ToList().Select(e => e.ErrorResult.ErrorMessage));
        CustomStatusGeneric customStatusGeneric = new CustomStatusGeneric();
        customStatusGeneric.IsValid = statusGeneric.IsValid;
        customStatusGeneric.Message = statusGeneric.IsValid ? statusGeneric.Message : errors;
        return customStatusGeneric;
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
