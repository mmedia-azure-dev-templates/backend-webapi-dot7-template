using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    private readonly ILocalizationService _localizationService;
    private ForgotPasswordResponse _forgotPasswordResponse;
    public ForgotPasswordHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator, ILocalizationService localizationService, IForgotPasswordResponse forgotPasswordResponse)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
        _localizationService = localizationService;
        _forgotPasswordResponse = (ForgotPasswordResponse)forgotPasswordResponse;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return _forgotPasswordResponse;
        }

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            _forgotPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("EmailNotConfirmed");
            _forgotPasswordResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTextError");
            return _forgotPasswordResponse;
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        MailStruct mailStruct = new MailStruct(
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email!
            },
            "Restableceer Password",
            "ForgotPasswordView"
           );

        ForgotPasswordMailData forgotPasswordMailData = new ForgotPasswordMailData()
        {
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email!,
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
