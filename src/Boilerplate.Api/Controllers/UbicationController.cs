using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class UbicationController : ControllerBase
{
    private readonly IContext _context;
    public UbicationController(IContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("provincias")]
    public async Task<List<GeographicLocation>> Provincias()
    {
        var result = await _context.GeographicLocations.Where(x => x.Parent == null).Where(x => x.Parroquia == 0).ToListAsync();
        return result;
    }
}
