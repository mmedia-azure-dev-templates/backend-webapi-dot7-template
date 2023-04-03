using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.JwtTokenCode;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.BaseCode.PermissionsCode;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Auth.Authenticate;
using Boilerplate.Application.Features.Auth.ConfirmEmail;
using Boilerplate.Application.Features.Auth.ForgotPassword;
using Boilerplate.Application.Features.Auth.GenerateConfirmation;
using Boilerplate.Application.Features.Auth.GenerateInitial;
using Boilerplate.Application.Features.Auth.ResetPassword;
using Boilerplate.Application.Features.Auth.UpdateEmail;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.PermissionsCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
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
    [ProducesResponseType(typeof(AuthenticateResponse), StatusCodes.Status200OK)]
    public async Task<AuthenticateResponse> Authenticate([FromBody] AuthenticateRequest request)
    {
        var result = await _mediator.Send(request);
        return result;
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
    public async Task<ForgotPasswordResponse> ForgotPassword([FromBody]ForgotPasswordRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("resetpassword")]
    public async Task<ActionResult<ResetPasswordResponse>> ResetPassword([FromBody]ResetPasswordRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("generateinitialconfirmation")]
    public async Task<ActionResult<GenerateInitialConfirmationResponse>> GenerateInitialConfirmation(GenerateInitialConfirmationRequest request)
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

    [HttpPost]
    [AllowAnonymous]
    [Route("confirmemail")]
    public async Task<ActionResult<ConfirmEmailResponse>> ConfirmEmail([FromBody] ConfirmEmailRequest request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// This migration oldEmail to newEmail
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HasPermission(DefaultPermissions.UserEmailUpdate)]
    [HttpPost]
    [Route("updateemail")]
    public async Task<ActionResult> UpdateEmail(UpdateEmailRequest request)
    {
        return Ok(await _mediator.Send(request));
    }

    [HttpGet]
    [Route("loggedin")]
    public ActionResult<bool> LoggedIn()
    {
        return true;
    }


    /// <summary>
    /// This returns the permission names for the current user (Force List not null available)
    /// This can be useful for your front-end to use the current user's Permissions to only expose links
    /// that the user has access too.
    /// You should call this after a login and when the JWT Token is refreshed
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("getuserpermissions")]
    public List<string> GetUsersPermissions([FromServices] IUsersPermissionsService service)
    {
        return service.PermissionsFromUser(User) ?? new List<string>();
    }
}
