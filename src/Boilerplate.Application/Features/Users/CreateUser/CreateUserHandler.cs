using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Auth;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using OneOf.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Users.CreateUser;

public class CreateUserHandler : IRequestHandler<CreateUsersInformationsRequest, UserResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateUserHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private UserResponse _userResponse;


    public CreateUserHandler(IContext context, IMapper mapper, ILogger<CreateUserHandler> logger, IMailService mail, UserManager<ApplicationUser> userManager, IUserResponse userResponse, ILocalizationService localizationService)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _userManager = userManager;
        _userResponse = (UserResponse)userResponse;
        _localizationService = localizationService;
    }
    public async Task<UserResponse> Handle(CreateUsersInformationsRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = request.Email,
                    Email = request.Email,
                    PasswordHash = request.Ndocument,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.Mobile,
                    LastLogin = DateTime.Now,
                };

                var resultUser = await _userManager.CreateAsync(user, request.Ndocument);

                if (!resultUser.Succeeded)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleError").Value;
                    return _userResponse;
                }

                UserInformation userInformation = new()
                {
                    UserId = user.Id,
                    TypeDocument = request.IdentificationType,
                    Nacionality = request.NacionalityType,
                    Ndocument = request.Ndocument,
                    Gender = request.GenderType,
                    CivilStatus = request.CivilStatusType,
                    BirthDate = request.BirthDate,
                    //EntryDate = request.EntryDate,
                    //DepartureDate = request.DepartureDate,
                    Hired = false,
                    //ImgUrl = request.ImgUrl,
                    //CurriculumUrl = request.CurriculumUrl,
                    Mobile = request.Mobile,
                    //Phone = request.Phone,
                    PrimaryStreet = request.PrimaryStreet,
                    SecondaryStreet = request.SecondaryStreet,
                    Numeration = request.Numeration,
                    Reference = request.Reference,
                    Provincia = request.Provincia,
                    Canton = request.Canton,
                    Parroquia = request.Parroquia,
                    //Notes = request.Notes
                };

                _context.UserInformations.Add(userInformation);
                await _context.SaveChangesAsync(cancellationToken);

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                var callbackUrl = new { token, email = user.Email };

                MailStruct mailData = new MailStruct(
                    user.Email,
                    user.FirstName + " " + user.LastName,
                    new List<string> {
                        user.Email
                    },
                    "Confirm your account",
                    "Welcome"
                   );

                // Create MailData object
                WelcomeMailData welcomeMail = new WelcomeMailData()
                {
                    Name = user.FirstName + " " + user.LastName,
                    Email = user.Email,
                    Token = token
                };

                bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

                if (!emailStatus)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value;
                    _userResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value;
                    _logger.LogInformation(3, _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value);
                }

                scope.Complete();
                _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleSuccess").Value;
                _userResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("UserResponseTitleSuccess").Value;
                _userResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
                _userResponse.Transaction = true;
                return _userResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                _logger.LogInformation(3, ex.Message);
                _userResponse.SweetAlert.Title = ex.Message;
                _userResponse.SweetAlert.Text = ex.Message;
                return _userResponse;
            }
        }
    }
}