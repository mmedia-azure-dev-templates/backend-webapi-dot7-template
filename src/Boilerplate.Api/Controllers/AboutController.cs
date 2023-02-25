using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutController : ControllerBase
{
    private readonly ILocalizationService _localizationService;

    public AboutController(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
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
        var article = _localizationService.GetLocalizedHtmlString("Article");

        return Ok(new { Article = article });
    }

}