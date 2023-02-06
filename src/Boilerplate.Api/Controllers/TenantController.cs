﻿using AuthPermissions.AdminCode;
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
//[Authorize]
public class TenantController : ControllerBase
{
    private readonly IAuthTenantAdminService _authTenantAdmin;

    public TenantController(IAuthTenantAdminService authTenantAdmin)
    {
        _authTenantAdmin = authTenantAdmin;
    }

    
    [HttpGet]
    [HasPermission(Example3Permissions.TenantList)]
    public async Task<IActionResult> Index(string message)
    {
        var tenantNames = await SingleLevelTenantDto.TurnIntoDisplayFormat(_authTenantAdmin.QueryTenants())
            .OrderBy(x => x.TenantName)
            .ToListAsync();
        return Ok(tenantNames);
    }

    [HttpPost]
    [Route("create")]
    [ValidateAntiForgeryToken]
    [HasPermission(Example3Permissions.TenantCreate)]
    public async Task<IActionResult> Create(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin.AddSingleTenantAsync(input.TenantName, input.TenantRolesName);
        
        return Ok(status);
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
    }

    [HasPermission(Example3Permissions.TenantUpdate)]
    [HttpGet]
    [Route("edit")]
    public async Task<IActionResult> Edit(int id)
    {
        return Ok();
        //return View(await SingleLevelTenantDto.SetupForUpdateAsync(_authTenantAdmin, id));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("edit")]
    [HasPermission(Example3Permissions.TenantUpdate)]
    public async Task<IActionResult> Edit(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin
            .UpdateTenantNameAsync(input.TenantId, input.TenantName);
        return Ok();
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
    }

    [HasPermission(Example3Permissions.TenantDelete)]
    [HttpGet]
    [Route("delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var status = await _authTenantAdmin.GetTenantViaIdAsync(id);
        //if (status.HasErrors)
        //    return RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() });
        return Ok();
        //return View(new SingleLevelTenantDto
        //{
        //    TenantId = id,
        //    TenantName = status.Result.TenantFullName
        //});
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("delete")]
    [HasPermission(Example3Permissions.TenantDelete)]
    public async Task<IActionResult> Delete(SingleLevelTenantDto input)
    {
        var status = await _authTenantAdmin.DeleteTenantAsync(input.TenantId);
        return Ok();
        //return status.HasErrors
        //    ? RedirectToAction(nameof(ErrorDisplay),
        //        new { errorMessage = status.GetAllErrors() })
        //    : RedirectToAction(nameof(Index), new { message = status.Message });
    }

    //[HasPermission(Example3Permissions.TenantAccessData)]
    

    //[HttpGet]
    //public ActionResult ErrorDisplay(string errorMessage)
    //{
    //    return Ok();
    //    //return View((object)errorMessage);
    //}
}
