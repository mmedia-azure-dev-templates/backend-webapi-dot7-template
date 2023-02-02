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
            NormalizedUserName = request.Email.ToUpper(),
            Email = request.Email,
            NormalizedEmail = request.Email.ToUpper(),
            PasswordHash = BC.HashPassword(request.Email),
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            LockoutEnabled = true,
            LastLogin = DateTime.Now,
        };

        Identification identification = new()
        {
            UserId = user.Id,
            CatTypeDocument = request.CatTypeDocument,
            CatNacionality = request.CatNacionality,
            Ndocument = request.Ndocument,
            CatGender = request.CatGender,
            CatCivilStatus = request.CatCivilStatus,
            BirthDate = request.BirthDate,
            EntryDate = request.EntryDate,
            DepartureDate = request.DepartureDate,
            Hired = request.Hired,
            ImgUrl = request.ImgUrl,
            CurriculumUrl = request.CurriculumUrl,
            Mobile = request.Mobile,
            Phone = request.Phone,
            Address = request.Address,
            UbcProvincia = request.UbcProvincia,
            UbcCanton = request.UbcCanton,
            UbcParroquia = request.UbcParroquia,
            Notes = request.Notes,
        };

        _context.ApplicationUsers.Add(user);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetUserResponse>(user);
    }
}