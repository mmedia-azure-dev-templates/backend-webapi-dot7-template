using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Boilerplate.Api.Controllers;


public class DefaultController : Controller
{
    [HttpGet]
    [Route("/")]
    [AllowAnonymous]
    public IActionResult Index()
    {
        return Ok("API SUCCESSFULLY STARTED");
    }

    //[HttpGet]
    //[Route("/Identity/Account/Login")]
    //[AllowAnonymous]
    //public IActionResult IdentityLogin()
    //{
    //    return View("Account/Start");
    //}
}
