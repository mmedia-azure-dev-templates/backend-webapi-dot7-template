using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Boilerplate.Application.Features.Utils.GeneratePassword;

public class GeneratePasswordHandler : RequestHandler<GeneratePasswordRequest,GeneratePasswordResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;


    public GeneratePasswordHandler(IMapper mapper, IContext context, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
    }
        
    protected override GeneratePasswordResponse Handle(GeneratePasswordRequest request)
    {
        var user = new ApplicationUser();
        var generatedPassword = _userManager.PasswordHasher.HashPassword(user, request.Password);
        return new GeneratePasswordResponse()
        {
            Password = generatedPassword
        };
    }
}