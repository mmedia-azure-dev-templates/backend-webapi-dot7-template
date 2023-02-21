//https://www.meziantou.net/using-hierarchyid-with-entity-framework-core.htm
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    [HttpPost]
    [Route("createchild")]
    public async Task<IActionResult> CreateChild([FromBody] Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return Ok();
    }
}
