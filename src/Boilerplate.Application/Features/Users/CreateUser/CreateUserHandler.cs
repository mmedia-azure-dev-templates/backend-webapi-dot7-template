﻿using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Emails;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Humanizer;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUsersIdenticationsRequest, GetUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMailService _mail;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    
    
    public CreateUserHandler(IContext context, IMapper mapper, ILogger<CreateUserHandler> logger, IMailService mail , SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _signInManager = signInManager;
        _userManager = userManager;
    }
    public async Task<GetUserResponse> Handle(CreateUsersIdenticationsRequest request, CancellationToken cancellationToken)
    {
        GetUserResponse userResponse = new GetUserResponse();

        using var transaction = _context.Database.BeginTransaction();

        try
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = request.Email,
                NormalizedUserName = request.Email.ToUpper(),
                Email = request.Email,
                NormalizedEmail = request.Email.ToUpper(),
                PasswordHash = request.Password,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneNumber = request.PhoneNumber,
                LockoutEnabled = true,
                LastLogin = DateTime.Now,
            };
            
            var userStatus = await _userManager.CreateAsync(user, request.Password);
            
            if (userStatus.Succeeded)
            {
                
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callbackUrl = new { userId = user.Id, code = code };

                MailData mailData = new MailData(
                    user.Email,
                    user.FirstName + " " + user.LastName,
                    new List<string> {
                        user.Email
                    },
                    null,
                    null,
                    null,
                    null,
                    "Confirm your account",
                    "Hola soy el body",
                    "Welcome"
                   );

                // Create MailData object
                WelcomeMail welcomeMail = new WelcomeMail()
                {
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Code = code
                };
                
                bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

                if (emailStatus)
                {
                    userResponse.Message = "Email success!";
                    _logger.LogInformation(3, "Email success!");
                }
                else
                {
                    userResponse.Message = "Email failed!";
                    _logger.LogInformation(3, "Email failed!");
                    throw new Exception("Email failed!");
                }


                //await _signInManager.SignInAsync(user, isPersistent: false);
                //_logger.LogInformation(3, "User created a new account with password.");
            }
            else
            {
                List<IdentityError> errorList = userStatus.Errors.ToList();
                var errors = string.Join(" | ", errorList.Select(e => e.Description));
                _logger.LogInformation(3, "Error create user Identity");
                throw new Exception(errors);
            }

            await _context.SaveChangesAsync(cancellationToken);

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
            userResponse.Transaction = true;
            return userResponse;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            userResponse.Message = ex.Message;
            return userResponse;
        }
    }
}