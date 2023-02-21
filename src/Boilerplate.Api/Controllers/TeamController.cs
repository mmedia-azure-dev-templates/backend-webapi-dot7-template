using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TeamController : ControllerBase
{
    private readonly IContext _context;
    public TeamController(IContext context)
    {
        _context = context;
        
    }

    [HttpPost]
    [Route("createchild")]
    public async Task<IActionResult> CreateChild([FromBody]Team team)
    {
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();
        return Ok("Hola");
    }
}
