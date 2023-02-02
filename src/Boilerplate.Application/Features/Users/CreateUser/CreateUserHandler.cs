using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Heroes;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUserRequest, GetUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;


    public CreateUserHandler(IMapper mapper, IContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _mapper = mapper;
        _context = context;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<GetUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        ApplicationUser user = new()
        {
            UserName = request.Email,
            Email = request.Email,
            PasswordHash = BC.HashPassword("P@ssw0rd1982"),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            LastLogin = DateTime.Now,
            NormalizedEmail = request.Email.ToUpper(),
            LockoutEnabled = true,
        };
        
        //var result = await _userManager.CreateAsync(user, "P@ssw0rd1982");
        //if (!result.Succeeded)
        //{
        //    var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
        //    throw new InvalidOperationException(
        //        $"Tried to add user {user.Email}, but failed. Errors:\n {errorDescriptions}");
        //}
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetUserResponse>(user);
    }
}