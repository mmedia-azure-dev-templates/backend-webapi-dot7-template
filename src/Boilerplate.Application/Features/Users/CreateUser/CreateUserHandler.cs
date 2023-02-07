using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Emails;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUsersIdenticationsRequest, GetUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    /// <summary>
    /// private readonly ILogger _logger;
    /// </summary>
    private readonly IMailService _mail;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    
    
    public CreateUserHandler(IContext context, IMapper mapper, /*ILogger logger,*/ IMailService mail , SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        //_logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<GetUserResponse> Handle(CreateUsersIdenticationsRequest request, CancellationToken cancellationToken)
    {
        using var transaction = _context.Database.BeginTransaction();

        try
        {
            var user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
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
            
            var result = await _userManager.CreateAsync(user, "Market2022$$");
            await _context.SaveChangesAsync(cancellationToken);

            if (result.Succeeded)
            {
                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                // Send an email with this link
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = new { userId = user.Id, code = code };
                // Create MailData object
                MailData mailData = new MailData(
                    to: new List<string> { 
                        user.Email
                    },
                    subject: "Confirm your account",
                    body: "Hola soy el body",
                    from: "raul.flores@mad.ec",
                    displayName: "Cirilo"
                    );
                bool sendResult = await _mail.SendAsync(mailData, new CancellationToken());
                var cirilo = 1;
                //await _mail.SendAsync(model.Email, "Confirm your account","Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
                //await _signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation(3, "User created a new account with password.");
            }
            

            //Identification identification = new()
            //{
            //    UserId = user.LegacyId,
            //    CatTypeDocument = request.CatTypeDocument,
            //    CatNacionality = request.CatNacionality,
            //    Ndocument = request.Ndocument,
            //    CatGender = request.CatGender,
            //    CatCivilStatus = request.CatCivilStatus,
            //    BirthDate = request.BirthDate,
            //    EntryDate = request.EntryDate,
            //    DepartureDate = request.DepartureDate,
            //    Hired = request.Hired,
            //    ImgUrl = request.ImgUrl,
            //    CurriculumUrl = request.CurriculumUrl,
            //    Mobile = request.Mobile,
            //    Phone = request.Phone,
            //    Address = request.Address,
            //    UbcProvincia = request.UbcProvincia,
            //    UbcCanton = request.UbcCanton,
            //    UbcParroquia = request.UbcParroquia,
            //    Notes = request.Notes,
            //};

            //_context.Identifications.Add(identification);

            //await _context.SaveChangesAsync(cancellationToken);
            transaction.Commit();
            return _mapper.Map<GetUserResponse>(user);
        }
        catch (Exception)
        {
            transaction.Rollback();
            throw;
        }
    }
}