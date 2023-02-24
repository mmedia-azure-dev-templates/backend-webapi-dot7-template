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

namespace Boilerplate.Application.Features.Auth.Forgot;
public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordRequest, ForgotPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    private readonly ILocalizationService _localizationService;
    public ForgotPasswordHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator, ILocalizationService localizationService)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
        _localizationService = localizationService;
    }

    public async Task<ForgotPasswordResponse> Handle(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        ForgotPasswordResponse forgotResponse = new ForgotPasswordResponse(_localizationService);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            
            return forgotResponse;
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
            forgotResponse.Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleSuccess");
            forgotResponse.Text = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTextSuccess");
            forgotResponse.Icon = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess");
            forgotResponse.Transaction = true;
        }
        
        return forgotResponse;
    }
}
