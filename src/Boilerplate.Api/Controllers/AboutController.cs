using Boilerplate.Api.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly IStringLocalizer<SharedResource> _sharedResourceLocalizer;

    public AboutController(IStringLocalizer<SharedResource> sharedResourceLocalizer)
    {
        _sharedResourceLocalizer = sharedResourceLocalizer;
    }

    /// <summary>
    /// This endpoint will access the SharedResourece to retrieve the localized data ...
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Route("SharedResource")]
    [AllowAnonymous]
    public IActionResult GetUsingSharedResource()
    {
        var article = _sharedResourceLocalizer["Article"].Value;
        var postName = _sharedResourceLocalizer.GetString("Welcome").Value ?? "Ramiro";

        return Ok(new { Article = article, PostName = postName });
    }

}