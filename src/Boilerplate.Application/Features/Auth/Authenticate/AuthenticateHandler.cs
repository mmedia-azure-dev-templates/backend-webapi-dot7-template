using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Auth.ForgotPassword;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace Boilerplate.Application.Features.Auth.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest, AuthenticateResponse>
{
    private readonly IContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly ILocalizationService _locationService;
    private AuthenticateResponse _authenticateResponse;

    public AuthenticateHandler(IContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenBuilder tokenBuilder, ILocalizationService localizationService, IAuthenticateResponse authenticateResponse)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenBuilder = tokenBuilder;
        _locationService = localizationService;
        _authenticateResponse = (AuthenticateResponse?)authenticateResponse;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {        
        var user = await _userManager.FindByEmailAsync(request.Email);
        _authenticateResponse.SweetAlert.Text = "";
        if (user == null)
        {
            _authenticateResponse.SweetAlert.Title = _locationService.GetLocalizedHtmlString("EmailNotExist").Value;
            return _authenticateResponse;
        }

        if (user != null)
        {
            if (!user.EmailConfirmed)
            {
                _authenticateResponse.SweetAlert.Title = _locationService.GetLocalizedHtmlString("EmailNotConfirmed").Value;
                return _authenticateResponse;
            }

        }

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
        if (!result.Succeeded)
        {
            _authenticateResponse.SweetAlert.Title = _locationService.GetLocalizedHtmlString("CredentialsNotValid").Value;
            return _authenticateResponse;
        }
        
        var token = await _tokenBuilder.GenerateJwtTokenAsync(user.Id.ToString());
        await _context.SaveChangesAsync(cancellationToken);

        _authenticateResponse.Token = token;
        _authenticateResponse.SweetAlert.Title = _locationService.GetLocalizedHtmlString("AuthenticateResponseTitleSuccess").Value;
        _authenticateResponse.SweetAlert.Text = _locationService.GetLocalizedHtmlString("AuthenticateResponseTitleSuccess").Value;
        _authenticateResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _locationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
        _authenticateResponse.Transaction = true;
        return _authenticateResponse;
    }
}