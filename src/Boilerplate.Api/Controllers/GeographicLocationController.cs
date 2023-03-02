using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
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
public class GeographicLocationController : ControllerBase
{
    private readonly IContext _context;
    public GeographicLocationController(IContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("provincias")]
    public async Task<List<GeographicLocation>> Provincias()
    {
        var result = await _context.GeographicLocations.Where(x => x.Parent == null).Where(x => x.Parroquia == 0).OrderBy(x => x.Name).ToListAsync();
        return result;
    }

    [HttpGet]
    [Route("cantones")]
    public async Task<List<GeographicLocation>> Cantones([FromQuery] GeographicLocationId cantonId)
    {
        var result = await _context.GeographicLocations.Where(x => x.Parent == cantonId.Value).OrderBy(x => x.Name).ToListAsync();
        return result;
    }

    [HttpGet]
    [Route("parroquias")]
    public async Task<List<GeographicLocation>> Parroquias([FromQuery]GeographicLocationId parroquiaId)
    {
        var result = await _context.GeographicLocations.Where(x => x.Parent == parroquiaId.Value).Where(x=>x.Parroquia==1).OrderBy(x => x.Name).ToListAsync();
        return result;
    }
}
