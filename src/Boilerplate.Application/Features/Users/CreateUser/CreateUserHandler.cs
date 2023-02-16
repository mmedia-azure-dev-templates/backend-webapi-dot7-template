using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using static org.apache.zookeeper.KeeperException;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUsersIdenticationsRequest, GetUserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMailService _mail;
    private readonly UserManager<ApplicationUser> _userManager;


    public CreateUserHandler(IContext context, IMapper mapper, ILogger<CreateUserHandler> logger, IMailService mail, UserManager<ApplicationUser> userManager)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _userManager = userManager;
    }
    public async Task<GetUserResponse> Handle(CreateUsersIdenticationsRequest request, CancellationToken cancellationToken)
    {
        GetUserResponse userResponse = new GetUserResponse();

        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PasswordHash = request.Password,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.PhoneNumber,
                    LastLogin = DateTime.Now,
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                

                UserInformation userInformation = new()
                {
                    UserId = Guid.Parse(user.Id),
                    TypeDocument = request.TypeDocument,
                    Nacionality = request.Nacionality,
                    Ndocument = request.Ndocument,
                    Gender = request.Gender,
                    CivilStatus = request.CivilStatus,
                    BirthDate = request.BirthDate,
                    EntryDate = request.EntryDate,
                    DepartureDate = request.DepartureDate,
                    Hired = request.Hired,
                    ImgUrl = request.ImgUrl,
                    CurriculumUrl = request.CurriculumUrl,
                    Mobile = request.Mobile,
                    Phone = request.Phone,
                    PrimaryStreet = request.PrimaryStreet,
                    SecondaryStreet = request.SecondaryStreet,
                    Numeration = request.Numeration,
                    Reference = request.Reference,
                    Provincia = request.Provincia,
                    Canton = request.Canton,
                    Parroquia = request.Parroquia,
                    Notes = request.Notes
                };
                _context.UserInformations.Add(userInformation);
                await _context.SaveChangesAsync(cancellationToken);

                if (result.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                    var callbackUrl = new { token, email = user.Email };

                    MailData mailData = new MailData(
                        user.Email,
                        user.FirstName + " " + user.LastName,
                        new List<string> {
                        user.Email
                        },
                        "Confirm your account",
                        "Welcome"
                       );

                    // Create MailData object
                    WelcomeMail welcomeMail = new WelcomeMail()
                    {
                        Name = user.FirstName + " " + user.LastName,
                        Email = user.Email,
                        Token = token
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

                }
                else
                {
                    List<IdentityError> errorList = result.Errors.ToList();
                    var errors = string.Join(" | ", errorList.Select(e => e.Description));
                    _logger.LogInformation(3, "Error create user Identity");
                    throw new Exception(errors);
                }
                scope.Complete();
                userResponse.Transaction = true;
                return userResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(3, ex.Message);
                userResponse.Message = ex.Message;
                return userResponse;
            }
        }
    }
}