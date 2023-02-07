using AuthPermissions.AdminCode;
using AuthPermissions.AspNetCore;
//using AuthPermissions.AspNetCore.AccessTenantData;
//using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities;
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
public class TenantController : ControllerBase
{
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public TenantController(IAuthTenantAdminService authTenantAdmin)
    {
        _authTenantAdmin = authTenantAdmin;
    }

    
    [HttpGet]
    [HasPermission(DefaultPermissions.TenantList)]
    public async Task<IActionResult> Index(string message)
    {
        var tenantNames = await SingleLevelTenantDto.TurnIntoDisplayFormat(_authTenantAdmin.QueryTenants())
            .OrderBy(x => x.TenantName)
            .ToListAsync();
        return Ok(tenantNames);
    }

    [HttpPost]
    [Route("create")]
    //[ValidateAntiForgeryToken]
    [HasPermission(DefaultPermissions.TenantCreate)]
    public async Task<IActionResult> Create(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin.AddSingleTenantAsync(input.TenantName, input.TenantRolesName);
        return Ok(status);
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
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
}
