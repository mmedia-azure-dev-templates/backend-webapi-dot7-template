using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.GenerateConfirmation;
public class GenerateConfirmationHandler : IRequestHandler<GenerateConfirmationRequest, GenerateConfirmationResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;

    public GenerateConfirmationHandler(UserManager<ApplicationUser> userManager, IMailService mail, ILocalizationService localizationService)
    {
        _userManager = userManager;
        _mail = mail;
        _localizationService = localizationService;
    }

    public async Task<GenerateConfirmationResponse> Handle(GenerateConfirmationRequest request, CancellationToken cancellationToken)
    {
        GenerateConfirmationResponse generateResponse = new GenerateConfirmationResponse();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            generateResponse.Message = "Email not found!";
            return generateResponse;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        MailStruct mailData = new MailStruct(
            user.Email,
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email
            },
            "Confirmar Email",
            "ConfirmationView"
           );

        ConfirmationMailData welcomeMail = new ConfirmationMailData()
        {
            Name = user.FirstName + " " + user.LastName,
            Email = user.Email,
            Token = token
        };
        bool emailStatus = await _mail.CreateEmailMessage(mailData, welcomeMail, new CancellationToken());

        if (!emailStatus)
        {
            generateResponse.Message = "Email failed!";
            generateResponse.Transaction = false;
            return generateResponse;
        }

        generateResponse.Message = "Check your Email Inbox!";
        generateResponse.Transaction = true;
        return generateResponse;
    }
}
