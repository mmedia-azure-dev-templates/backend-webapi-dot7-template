using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers;

[Route("/")]
[AllowAnonymous]
public class DefaultController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Ok("API SUCCESSFULLY STARTED");
    }
}
