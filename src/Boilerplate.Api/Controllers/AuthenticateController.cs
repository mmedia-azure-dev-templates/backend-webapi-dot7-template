using AuthPermissions;
using AuthPermissions.AspNetCore.JwtTokenCode;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.BaseCode.PermissionsCode;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Auth.Authenticate;
using Boilerplate.Application.Features.Auth.Confirm;
using Boilerplate.Application.Features.Auth.Forgot;
using Boilerplate.Application.Features.Auth.Generate;
using Boilerplate.Application.Features.Auth.GenerateConfirmation;
using Boilerplate.Application.Features.Auth.Reset;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase
{
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;

    public AuthenticateController(IMailService mail,IMediator mediator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenBuilder tokenBuilder, IClaimsCalculator claimsCalculator)
    {
        _mail = mail;
        _mediator = mediator;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenBuilder = tokenBuilder;
    }

    /// <summary>
    /// This checks you are a valid user and returns a JTW token
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticate")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Authenticate([FromQuery] AuthenticateRequest request)
    {
        var result = await _mediator.Send(request);
        return result.Match<IActionResult>(
            valid => Ok(valid),
            notFound => NotFound()
        );
    }

    /// <summary>
    /// This checks you are a valid user and returns a JTW token and a Refresh token
    /// </summary>
    /// <param name="loginUser"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("authenticatewithrefresh")]
    public async Task<ActionResult<TokenAndRefreshToken>> AuthenticateWithRefresh(LoginUserModel loginUser)
    {
        //NOTE: The _signInManager.PasswordSignInAsync does not change the current ClaimsPrincipal - that only happens on the next access with the token
        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, false);
        if (!result.Succeeded)
        {
            return BadRequest(new { message = "Username or password is incorrect" });
        }
        var user = await _userManager.FindByEmailAsync(loginUser.Email);

        return await _tokenBuilder.GenerateTokenAndRefreshTokenAsync(user.Id);
    }

    /// <summary>
    /// This will refresh the JWT token using the provided Refresh token
    /// </summary>
    /// <param name="tokenAndRefresh"></param>
    /// <returns></returns>
    [AllowAnonymous]
    [HttpPost]
    [Route("refreshauthentication")]
    public async Task<ActionResult<TokenAndRefreshToken>> RefreshAuthentication(TokenAndRefreshToken tokenAndRefresh)
    {
        var result = await _tokenBuilder.RefreshTokenUsingRefreshTokenAsync(tokenAndRefresh);
        if (result.updatedTokens != null)
            return result.updatedTokens;

        return StatusCode(result.HttpStatusCode);
    }

    /// <summary>
    /// This will mark the JST refresh as used, so the user cannot refresh the JWT Token.
    /// </summary>
    /// <returns></returns>
    [Authorize]
    [HttpPost]
    [Route("logout")]
    public async Task<ActionResult> Logout([FromServices] IDisableJwtRefreshToken service, string refreshToken)
    {
        await service.LogoutUserViaRefreshTokenAsync(refreshToken);

        return Ok();
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("forgotpassword")]
    public async Task<ActionResult<ForgotResponse>> ForgotPassword([FromBody]ForgotRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("resetpassword")]
    public async Task<ActionResult<ResetResponse>> ResetPassword([FromBody]ResetRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("generatetoken")]
    public async Task<ActionResult<GenerateResponse>> GenerateToken(GenerateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("generateconfirmation")]
    public async Task<ActionResult<GenerateConfirmationResponse>> GenerateConfirmation(GenerateConfirmationRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("confirmemail")]
    public async Task<ActionResult<ConfirmResponse>> ConfirmEmail([FromQuery] ConfirmRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    [Route("loggedin")]
    public ActionResult<bool> LoggedIn()
    {
        return true;
    }


    /// <summary>
    /// This returns the permission names for the current user (or null if not available)
    /// This can be useful for your front-end to use the current user's Permissions to only expose links
    /// that the user has access too.
    /// You should call this after a login and when the JWT Token is refreshed
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("getuserpermissions")]
    public ActionResult<List<string>> GetUsersPermissions([FromServices] IUsersPermissionsService service)
    {
        return service.PermissionsFromUser(User);
    }
}
