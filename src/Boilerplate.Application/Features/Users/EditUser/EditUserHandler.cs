using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Auth.UpdateEmail;
using Boilerplate.Application.Features.Users.AvailableUserEmail;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Users.EditUser;

public class EditUserHandler : IRequestHandler<EditUserRequest, EditUserResponse>
{
    private readonly IMediator _mediator;
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<EditUserHandler> _logger;
    private readonly ILocalizationService _localizationService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IAwsS3Service _awsS3Service;
    private EditUserResponse _editUserResponse;


    public EditUserHandler(IMediator mediator, IContext context, IMapper mapper, ILogger<EditUserHandler> logger, UserManager<ApplicationUser> userManager, IEditUserResponse editUserResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _editUserResponse = (EditUserResponse)editUserResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
        _mediator = mediator;
    }
    public async Task<EditUserResponse> Handle(EditUserRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                ApplicationUser applicationUser = await _userManager.FindByIdAsync(request.UserId.ToString());
                if (request.Email != applicationUser.Email)
                {
                    var availableUserEmailResponse = await _mediator.Send(new AvailableUserEmailRequest(request.Email));
                    if (availableUserEmailResponse.IsAvailable == true)
                    {
                        await _mediator.Send(new UpdateEmailRequest
                        {
                            Email = applicationUser.Email,
                            EmailReplace = request.Email
                        });
                    }
                }

                applicationUser.FirstName = request.FirstName;
                applicationUser.LastName = request.LastName;
                applicationUser.PhoneNumber = request.Mobile;
                await _userManager.UpdateAsync(applicationUser);

                AmazonObject objectImageProfile = await _awsS3Service.UploadFileBase64Async(request.ImageProfile, "users/" + applicationUser.Id.ToString(), "fotoperfil.jpg");

                UserInformation userInformation = await _context.UserInformations.Where(x => x.UserId == request.UserId).FirstOrDefaultAsync();
                userInformation.DocumentType = request.DocumentType;
                userInformation.Nacionality = request.NacionalityType;
                userInformation.Ndocument = request.Ndocument;
                userInformation.Gender = request.GenderType;
                userInformation.CivilStatus = request.CivilStatusType;
                userInformation.BirthDate = request.BirthDate;
                userInformation.ImgUrl = objectImageProfile.ObjectUrl;
                userInformation.Mobile = request.Mobile;
                userInformation.PrimaryStreet = request.PrimaryStreet;
                userInformation.SecondaryStreet = request.SecondaryStreet;
                userInformation.Numeration = request.Numeration;
                userInformation.Reference = request.Reference;
                userInformation.Provincia = request.Provincia;
                userInformation.Canton = request.Canton;
                userInformation.Parroquia = request.Parroquia;

                _context.UserInformations.Update(userInformation);
                await _context.SaveChangesAsync(cancellationToken);

                scope.Complete();
                _editUserResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleSuccess").Value;
                _editUserResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("UserResponseTitleSuccess").Value;
                _editUserResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
                _editUserResponse.Transaction = true;
                return _editUserResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                _editUserResponse.SweetAlert.Title = ex.Message;
                _editUserResponse.SweetAlert.Text = ex.Message;
                return _editUserResponse;
            }
        }
    }
}