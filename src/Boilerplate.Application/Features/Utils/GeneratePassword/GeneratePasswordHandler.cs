using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Utils.GeneratePassword;

public class GeneratePasswordHandler : IRequestHandler<GeneratePasswordRequest,GeneratePasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;

    public GeneratePasswordHandler(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<GeneratePasswordResponse> Handle(GeneratePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser();
        var generatedPassword = _userManager.PasswordHasher.HashPassword(user, request.Password);
        return new GeneratePasswordResponse()
        {
            Password = generatedPassword
        };
    }
}