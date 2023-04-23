//https://www.meziantou.net/using-hierarchyid-with-entity-framework-core.htm
//https://www.mssqltips.com/sqlservertip/6048/sql-server-hierarchyid-data-type-overview-and-examples/
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
    public async Task<IActionResult> GetParent(UserId childId)
    {
        var child = await _context.Teams.Where(x => x.UserId == childId).FirstOrDefaultAsync();
        if(child == null)
        {
            return NotFound();
        }
        var parent = await _context.Teams
                .FirstOrDefaultAsync(e => e.HierarchyId == child.HierarchyId.GetAncestor(1));
        return Ok(parent);
    }

    [HttpGet]
    [Route("getchildren")]
    public async Task<IActionResult> GetChildren(UserId parentId)
    {
        var parent = await _context.Teams.Where(x => x.UserId == parentId).FirstOrDefaultAsync();
        if (parent == null)
        {
            return NotFound();
        }
        var childrens = await _context.Teams
            .Where(x => x.HierarchyId.GetAncestor(1) == parent.HierarchyId)
            .ToListAsync();
        return Ok(childrens);
    }

    [HttpGet]
    [Route("getdescendants")]
    public async Task<IActionResult> GetDescendants(UserId parentId)
    {
        var manager = await _context.Teams.Where(x => x.UserId == parentId).FirstOrDefaultAsync();
        if (manager == null)
        {
            return NotFound();
        }
        // Parent is considered its own descendant which means that this query returns the manager (Id = 2)
        var result = await _context.Teams
                .Where(employee => employee.HierarchyId.IsDescendantOf(manager.HierarchyId))
                .ToListAsync();
        return Ok(result);
    }

    [HttpGet]
    [Route("changeparent")]
    public async Task<IActionResult> ChangeParent(UserId oldParent, UserId newParent)
    {
        var oldManager = await _context.Teams.Where(x => x.UserId == oldParent).FirstOrDefaultAsync();
        var newManager = await _context.Teams.Where(x => x.UserId == newParent).FirstOrDefaultAsync();

        if(oldManager == null || newManager == null)
        {
            return NotFound();
        }

        var managees = await _context.Teams
                .Where(e => e.HierarchyId != oldManager.HierarchyId
                         && e.HierarchyId.IsDescendantOf(oldManager.HierarchyId))
                .ToListAsync();

        foreach (var child in managees)
        {
            child.HierarchyId = child.HierarchyId.GetReparentedValue(oldManager.HierarchyId, newManager.HierarchyId);
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet]
    [Route("getdepthlevel")]
    public async Task<IActionResult> GetDepthLevel(UserId child)
    {
        var employee = await _context.Teams.Where(x => x.UserId == child).FirstOrDefaultAsync();
        if(employee == null)
        {
            return NotFound();
        }
        //Console.WriteLine(employee.HierarchyId + " Level: " + employee.HierarchyId.GetLevel());
        return Ok(employee.HierarchyId + " Level: " + employee.HierarchyId.GetLevel());
    }

    [HttpGet]
    [Route("getalldepthlevel")]
    public async Task<IActionResult> GetAllDepthLevel(int level)
    {
        var childsAllLevel = await _context.Teams
        .Where(e => e.HierarchyId.GetLevel() == level)
        .ToListAsync();
        return Ok(childsAllLevel);
    }

    [HttpGet]
    [Route("getallhierarchyid")]
    public async Task<IActionResult> GetAllHierarchyId()
    {
        var result = await _context.Teams
        .OrderBy(employee => employee.HierarchyId)
        .ToListAsync();
        return Ok(result);
    }


    [HttpPost]
    [Route("createroot")]
    public async Task<IActionResult> CreateRoot(string email)
    {
        var user = await _context.ApplicationUsers.Where(x => x.Email == email).FirstOrDefaultAsync();
        if(user == null)
        {
            return NotFound();
        }
        Team root = new Team
        {
            UserId = new UserId(user.Id),
            HierarchyId = HierarchyId.Parse("/"),
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
        if (manager == null)
        {
            return NotFound();
        }
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
