using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Application.Features.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using MediatR;
using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.WebUtilities;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Entities.Enums;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, IForgotPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    private readonly ILocalizationService _localizationService;
    private IForgotPasswordResponse _forgotPasswordResponse;
    public ForgotPasswordHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator, ILocalizationService localizationService, IForgotPasswordResponse forgotPasswordResponse)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
        _localizationService = localizationService;
        _forgotPasswordResponse = forgotPasswordResponse;
    }

    public async Task<IForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        _forgotPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleSuccess");
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !await _userManager.IsEmailConfirmedAsync(user))
        {
            return _forgotPasswordResponse;
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        MailStruct mailStruct = new MailStruct(
            user.Email,
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email
            },
            "Restableceer Password",
            "ForgotPasswordView"
           );

        ForgotPasswordMailData forgotPasswordMailData = new ForgotPasswordMailData()
        {
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email,
            Token = token
        };
        bool emailStatus = await _mail.CreateEmailMessage(mailStruct, forgotPasswordMailData, new CancellationToken());

        if (emailStatus)
        {
            _forgotPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleSuccess");
            _forgotPasswordResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTextSuccess");
            _forgotPasswordResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
            _forgotPasswordResponse.Transaction = true;
        }

        return _forgotPasswordResponse;
    }
}
