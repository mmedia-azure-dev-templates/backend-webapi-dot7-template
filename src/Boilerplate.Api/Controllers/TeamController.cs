//https://www.meziantou.net/using-hierarchyid-with-entity-framework-core.htm
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TeamController : ControllerBase
{
    private readonly IContext _context;
    public TeamController(IContext context)
    {
        _context = context;
        
    }

    [HttpGet]
    [Route("getparent")]
    public async Task<IActionResult> GetParent()
    {
        var employee = await _context.Teams.FindAsync(2);

        var manager = await _context.Teams
                .FirstOrDefaultAsync(e => e.HierarchyId == employee.HierarchyId.GetAncestor(1));
        return Ok(manager);
    }

    [HttpGet]
    [Route("getchildren")]
    public async Task<IActionResult> GetChildren()
    {
        var manager = await _context.Teams.FindAsync(2);
        var employees = await _context.Teams
            .Where(employee => employee.HierarchyId.GetAncestor(1) == manager.HierarchyId)
            .ToListAsync();
        return Ok(employees);
    }

    [HttpGet]
    [Route("getdescendants")]
    public async Task<IActionResult> GetDescendants()
    {
        var manager = await _context.Teams.FindAsync(2);

        // Parent is considered its own descendant which means that this query returns the manager (Id = 2)
        var result = await _context.Teams
                .Where(employee => employee.HierarchyId.IsDescendantOf(manager.HierarchyId))
                .ToListAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("changeparent")]
    public async Task<IActionResult> ChangeParent()
    {
        var oldManager = await _context.Teams.FindAsync(2);
        var newManager = await _context.Teams.FindAsync(3);

        var managees = await _context.Teams
                .Where(e => e.HierarchyId != oldManager.HierarchyId
                         && e.HierarchyId.IsDescendantOf(oldManager.HierarchyId))
                .ToListAsync();

        foreach (var employee in managees)
        {
            employee.OldHierarchyId = employee.HierarchyId;
            employee.HierarchyId = employee.HierarchyId.GetReparentedValue(oldManager.HierarchyId, newManager.HierarchyId);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("getdepthlevel")]
    public async Task<IActionResult> GetDepthLevel()
    {
        var employee = await _context.Teams.FindAsync(6);

        Console.WriteLine(employee.HierarchyId + " Level: " + employee.HierarchyId.GetLevel());
        // /1/2/1/ Level: 3
        var employeesOfLevel2 = await _context.Teams
        .Where(e => e.HierarchyId.GetLevel() == 2)
        .ToListAsync();
        return Ok();
    }

    [HttpPost]
    [Route("createroot")]
    public async Task<IActionResult> CreateRoot(string email)
    {
        var user = await _context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
        Team root = new Team
        {
            UserId = user.Id,
            HierarchyId = HierarchyId.Parse("/"),
            OldHierarchyId = HierarchyId.Parse("/"),
        };
        _context.Teams.Add(root);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    [Route("createchild")]
    public async Task<IActionResult> CreateChild(UserId managerId,UserId childId)
    {
        var manager = await _context.Teams.Where(x => x.UserId == managerId).FirstOrDefaultAsync();
        var child = new Team 
        { 
            UserId = childId, 
            HierarchyId = manager.HierarchyId.GetDescendant(null, null) 
        };
        _context.Teams.Add(child);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
