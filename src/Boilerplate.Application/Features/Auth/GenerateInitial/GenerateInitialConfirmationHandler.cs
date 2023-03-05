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

namespace Boilerplate.Application.Features.Auth.GenerateInitial;
public class GenerateInitialConfirmationHandler : IRequestHandler<GenerateInitialConfirmationRequest, GenerateInitialConfirmationResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    public GenerateInitialConfirmationHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
    }

    public async Task<GenerateInitialConfirmationResponse> Handle(GenerateInitialConfirmationRequest request, CancellationToken cancellationToken)
    {
        GenerateInitialConfirmationResponse generateResponse = new GenerateInitialConfirmationResponse();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            generateResponse.Message = "Email not found!";
            return generateResponse;
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        MailStruct mailData = new MailStruct(
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
            generateResponse.Message = "Email failed!";
            return generateResponse;
        }

        generateResponse.Message = "Email sent!";
        generateResponse.Transaction = true;
        return generateResponse;
    }
}
