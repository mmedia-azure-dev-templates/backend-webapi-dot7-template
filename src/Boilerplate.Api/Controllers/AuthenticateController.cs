﻿using AuthPermissions;
using AuthPermissions.AspNetCore.JwtTokenCode;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions.BaseCode.PermissionsCode;
using Boilerplate.Application.Emails;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Auth.Authenticate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Boilerplate.Infrastructure.Reverse;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Identity.UI.V5.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

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
    private readonly IEmailSender _emailSender;

    public AuthenticateController(IMailService mail,IMediator mediator, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenBuilder tokenBuilder, IClaimsCalculator claimsCalculator, IEmailSender emailSender)
    {
        _mail = mail;
        _mediator = mediator;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenBuilder = tokenBuilder;
        _emailSender = emailSender;
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

        return Ok(await _tokenBuilder.GenerateTokenAndRefreshTokenAsync(user.Id));
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

    [HttpPost]
    [AllowAnonymous]
    [Route("resetpassword")]
    public async Task<IActionResult> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            return Ok("Error Forgot Password");
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = new { token, email = user.Email };
        MailData mailData = new MailData(
            user.Email,
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email
            },
            "Forgot Password",
            "Hola soy el body",
            "Welcome"
           );
        // Create MailData object
        WelcomeMail welcomeMail = new WelcomeMail()
        {
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email,
            Token = token
        };
        bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

        if (emailStatus)
        {
            return Ok("Email sent");
        }
        else
        {
            return Ok("Email failed!");
        }
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
    {
        var user = await _userManager.FindByEmailAsync(resetPassword.Email);
        if (user == null)
            return Ok("Email failed!");

        var resetPassResult = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
        if (!resetPassResult.Succeeded)
        {
            return Ok("Reset Password failed!");
        }

        return Ok("Reset Password success!");
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("generatetoken")]
    public async Task<IActionResult> GenerateToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Ok("Error");
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        var callbackUrl = new { token, email = user.Email };
        MailData mailData = new MailData(
            user.Email,
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email
            },
            "Confirm your account",
            "Hola soy el body",
            "Welcome"
           );
        // Create MailData object
        WelcomeMail welcomeMail = new WelcomeMail()
        {
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email,
            Token = token
        };
        bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

        if (emailStatus)
        {
            return Ok("Email sent");
        }
        else
        {
           return Ok("Email failed!");
        }
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("confirmemail")]
    public async Task<IActionResult> ConfirmEmail(string token, string email)
    {
        var bytes = WebEncoders.Base64UrlDecode(token);
        token = Encoding.UTF8.GetString(bytes);
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Ok("Error");
        var result = await _userManager.ConfirmEmailAsync(user, token);
        return Ok(result.Succeeded ? nameof(ConfirmEmail) : "Error");
    }
}
