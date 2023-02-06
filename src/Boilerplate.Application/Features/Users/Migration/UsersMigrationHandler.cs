using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.Migration;
public class CreateUsersMigration : IRequestHandler<UsersMigrationRequest, UsersMigrationResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IContext _context;
    public CreateUsersMigration(IContext context,UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<UsersMigrationResponse> Handle(UsersMigrationRequest request, CancellationToken cancellationToken)
    {
        if (request.PasswordMigration != "P@ssw0rd1982")
        {
            return new UsersMigrationResponse()
            {
                Message = "Password Migration Invalid"
            };
        }
        var Emails = await _context.ApplicationUsers.Select(x => x.Email).ToListAsync(cancellationToken);

        foreach (var email in Emails)
        {
            var user = _context.ApplicationUsers.Single(x => x.Email == email);
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.Email!);
            await _context.SaveChangesAsync(cancellationToken);
        }

        return new UsersMigrationResponse() {
            Message = "Password Migration Completed"
        };
    }
}
