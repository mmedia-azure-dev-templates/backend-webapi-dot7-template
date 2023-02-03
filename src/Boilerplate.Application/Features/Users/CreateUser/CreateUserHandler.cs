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

public class CreateUserHandler : IRequestHandler<CreateUsersIdenticationsRequest, GetUserResponse>
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
    public async Task<GetUserResponse> Handle(CreateUsersIdenticationsRequest request, CancellationToken cancellationToken)
    {
        
        ApplicationUser user = new()
        {
            LegacyId = 1,
            UserName = request.User.Email,
            NormalizedUserName = request.User.Email.ToUpper(),
            Email = request.User.Email,
            NormalizedEmail = request.User.Email.ToUpper(),
            PasswordHash = BC.HashPassword(request.User.Email),
            FirstName = request.User.FirstName,
            LastName = request.User.LastName,
            PhoneNumber = request.User.PhoneNumber,
            LockoutEnabled = true,
            LastLogin = DateTime.Now,
        };

        string userId = _context.ApplicationUsers.Add(user).Entity.Id;

        Identification identification = new()
        {
            UserId = 1,
            CatTypeDocument = request.Identification.CatTypeDocument,
            CatNacionality = request.Identification.CatNacionality,
            Ndocument = request.Identification.Ndocument,
            CatGender = request.Identification.CatGender,
            CatCivilStatus = request.Identification.CatCivilStatus,
            BirthDate = request.Identification.BirthDate,
            EntryDate = request.Identification.EntryDate,
            DepartureDate = request.Identification.DepartureDate,
            Hired = request.Identification.Hired,
            ImgUrl = request.Identification.ImgUrl,
            CurriculumUrl = request.Identification.CurriculumUrl,
            Mobile = request.Identification.Mobile,
            Phone = request.Identification.Phone,
            Address = request.Identification.Address,
            UbcProvincia = request.Identification.UbcProvincia,
            UbcCanton = request.Identification.UbcCanton,
            UbcParroquia = request.Identification.UbcParroquia,
            Notes = request.Identification.Notes,
        };

        
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<GetUserResponse>(user);
    }
}