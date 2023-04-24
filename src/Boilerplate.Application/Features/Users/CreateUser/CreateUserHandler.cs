using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
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
    private readonly IAddNewUserManager _addNewUserManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IEncryptDecryptService _encryptService;
    private UserResponse _userResponse;


    public CreateUserHandler(IEncryptDecryptService encryptService,IAddNewUserManager addNewUserManager,IContext context, IMapper mapper, ILogger<CreateUserHandler> logger, IMailService mail, UserManager<ApplicationUser> userManager, IUserResponse userResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _userManager = userManager;
        _userResponse = (UserResponse)userResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
        _addNewUserManager = addNewUserManager;
        _encryptService = encryptService;
    }
    public async Task<UserResponse> Handle(CreateUsersInformationsRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                ApplicationUser user = new ApplicationUser()
                {
                    UserName = request.Email.ToString(),
                    Email = request.Email.ToString(),
                    PasswordHash = request.Ndocument,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    PhoneNumber = request.Mobile,
                    EmailConfirmed = true,
                };

                var resultUser = await _userManager.CreateAsync(user, request.Ndocument);

                if (!resultUser.Succeeded)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleError").Value;
                    return _userResponse;
                }

                AmazonObject objectImageProfile = await _awsS3Service.UploadFileBase64Async(request.ImageProfile, "users/" + user.Id.ToString(), "fotoperfil.jpg");
                if (objectImageProfile.ObjectUrl == null)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleError").Value;
                    return _userResponse;
                }
                UserInformation userInformation = new()
                {
                    UserId = new UserId(user.Id),
                    DocumentType = request.DocumentType,
                    Nacionality = request.NacionalityType,
                    Ndocument = request.Ndocument,
                    Gender = request.GenderType,
                    CivilStatus = request.CivilStatusType,
                    BirthDate = request.BirthDate,
                    //EntryDate = request.EntryDate,
                    //DepartureDate = request.DepartureDate,
                    Hired = false,
                    ImgUrl = objectImageProfile.ObjectUrl,
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

                var normalizedEmail = request.Email.Trim().ToLower();
                var decrypted = _encryptService.Decrypt(Base64UrlEncoder.Decode(request.Invitation));
                var newUserData = JsonSerializer.Deserialize<AddNewUserDto>(decrypted);

                if(newUserData == null)
                {
                    return _userResponse;
                }

                if (newUserData.Email != normalizedEmail)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("InvitationEmailNotMatch").Value;
                    _userResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("InvitationEmailNotMatch").Value;
                    _logger.LogInformation(3, _localizationService.GetLocalizedHtmlString("InvitationEmailNotMatch").Value);
                    return _userResponse;
                }

                if (newUserData.TimeInviteExpires != default && newUserData.TimeInviteExpires < DateTime.UtcNow.Ticks)
                {
                    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("InvitationExpired").Value;
                    _userResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("InvitationExpired").Value;
                    _logger.LogInformation(3, _localizationService.GetLocalizedHtmlString("InvitationExpired").Value);
                    return _userResponse;
                }

                await _addNewUserManager.SetUserInfoAsync(newUserData);


                //var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                //var callbackUrl = new { token, email = user.Email };

                //MailStruct mailData = new MailStruct(
                //    user.FirstName + " " + user.LastName,
                //    new List<string> {
                //        user.Email
                //    },
                //    "Confirm your account",
                //    "WelcomeView"
                //   );

                //// Create MailData object
                //WelcomeMailData welcomeMail = new WelcomeMailData()
                //{
                //    Name = user.FirstName + " " + user.LastName,
                //    Email = user.Email,
                //    Token = token
                //};

                //bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

                //if (!emailStatus)
                //{
                //    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value;
                //    _userResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value;
                //    _logger.LogInformation(3, _localizationService.GetLocalizedHtmlString("UserResponseEmailError").Value);
                //    return _userResponse;
                //}

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