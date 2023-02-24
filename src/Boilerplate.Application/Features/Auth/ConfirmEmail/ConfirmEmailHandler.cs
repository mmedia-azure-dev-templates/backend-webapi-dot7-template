using Boilerplate.Application.Features.Auth.Forgot;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.Confirm;
public class ConfirmEmailHandler: IRequestHandler<ConfirmEmailRequest, ConfirmEmailResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    public ConfirmEmailHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
    }

    public async Task<ConfirmEmailResponse> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        ConfirmEmailResponse confirmResponse = new ConfirmEmailResponse();
        var bytes = WebEncoders.Base64UrlDecode(request.Token);
        request.Token = Encoding.UTF8.GetString(bytes);
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            confirmResponse.Message = "User not found!";
            return confirmResponse;
        }
            
        var result = await _userManager.ConfirmEmailAsync(user, request.Token);
        
        if (!result.Succeeded)
        {
            confirmResponse.Message = "Confirm email failed!";
            return confirmResponse;
        }
        confirmResponse.Message = "Confirm email success!";
        confirmResponse.Transaction = true;
        return confirmResponse;
    }
}
