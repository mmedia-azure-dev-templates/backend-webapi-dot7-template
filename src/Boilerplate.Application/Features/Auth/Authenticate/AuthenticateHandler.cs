using AuthPermissions.AspNetCore.JwtTokenCode;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OneOf;
using System.Threading;
using System.Threading.Tasks;


namespace Boilerplate.Application.Features.Augh.Authenticate;

public class AuthenticateHandler : IRequestHandler<AuthenticateRequest,OneOf<AuthenticateResponse, AuthenticateNotFound>>
{
    private readonly IContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;

    public AuthenticateHandler(IContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ITokenBuilder tokenBuilder)
    {
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
        _tokenBuilder = tokenBuilder;
    }

    public async Task<OneOf<AuthenticateResponse, AuthenticateNotFound>> Handle(AuthenticateRequest request, CancellationToken cancellationToken)
    {
        //NOTE: The _signInManager.PasswordSignInAsync does not change the current ClaimsPrincipal - that only happens on the next access with the token
        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, false);
        if (!result.Succeeded)
        {
            return new AuthenticateNotFound() { Message = "Email or Password incorrect"};
        }
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return new AuthenticateNotFound() { Message = "User not found" };
        }
        var token = await _tokenBuilder.GenerateJwtTokenAsync(user.Id);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthenticateResponse()
        {
            Token = token
        };

        //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email.ToLower() == request.Email.ToLower(), cancellationToken);
        //if (user == null || !BC.Verify(request.Password, user.Password))
        //{
        //    return null;
        //}

        //var tokenHandler = new JwtSecurityTokenHandler();
        //var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //var claims = new ClaimsIdentity(new Claim[]
        //{
        //new(ClaimTypes.NameIdentifier, user.Id.ToString()),
        //new(ClaimTypes.Email, user.Email),
        //new(ClaimTypes.Role, user.Role)
        //});

        //var expDate = DateTime.UtcNow.AddHours(1);

        //var tokenDescriptor = new SecurityTokenDescriptor
        //{
        //    Subject = claims,
        //    Expires = expDate,
        //    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //    Audience = _appSettings.Audience,
        //    Issuer = _appSettings.Issuer
        //};
        //var token = tokenHandler.CreateToken(tokenDescriptor);
        //await _context.SaveChangesAsync(cancellationToken);
        //return _mapper.Map<AuthenticateResponse?>(token);

        //return new Jwt
        //{
        //    Token = tokenHandler.WriteToken(token),
        //    ExpDate = expDate
        //};
    }
}