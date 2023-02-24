using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using OneOf;
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

    public AuthenticateHandler(IContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenBuilder tokenBuilder, ILocalizationService localizationService)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenBuilder = tokenBuilder;
        _locationService = localizationService;
    }

    public async Task<AuthenticateResponse> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        AuthenticateResponse authenticateResponse = new AuthenticateResponse();
        
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            authenticateResponse.Message = _locationService.GetLocalizedHtmlString("EmailNotExist").Value;
            return authenticateResponse;
        }

        if (user != null)
        {
            if (!user.EmailConfirmed)
            {
                authenticateResponse.Message = _locationService.GetLocalizedHtmlString("EmailNotConfirmed").Value;
                return authenticateResponse;
            }

        }

        //NOTE: The _signInManager.PasswordSignInAsync does not change the current ClaimsPrincipal - that only happens on the next access with the token
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
        if (!result.Succeeded)
        {
            authenticateResponse.Message = _locationService.GetLocalizedHtmlString("CredentialsNotValid").Value;
            return authenticateResponse;
        }
        
        var token = await _tokenBuilder.GenerateJwtTokenAsync(user.Id.ToString());
        await _context.SaveChangesAsync(cancellationToken);

        authenticateResponse.Token = token;
        authenticateResponse.Transaction = true;
        return authenticateResponse;
    }
}