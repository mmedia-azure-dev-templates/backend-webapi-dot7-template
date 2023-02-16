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

namespace Boilerplate.Application.Features.Auth.Forgot;
public class ForgotHandler : IRequestHandler<ForgotRequest, ForgotResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    public ForgotHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
    }

    public async Task<ForgotResponse> Handle(ForgotRequest request, CancellationToken cancellationToken)
    {
        ForgotResponse forgotResponse = new ForgotResponse();
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
        {
            // Don't reveal that the user does not exist or is not confirmed
            forgotResponse.Message = "Error Forgot Password";
            return forgotResponse;
        }
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        token = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        MailData mailData = new MailData(
            user.Email,
            user.FirstName + " " + user.LastName,
            new List<string> {
                        user.Email
            },
            "Forgot Password",
            "ForgotPassword"
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
            forgotResponse.Message = "Email send!";
            forgotResponse.Transaction = true;
        }
        else
        {
            forgotResponse.Message = "Email failed!";
        }
        
        return forgotResponse;
    }
}
