﻿using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ConfirmEmail;
public class ConfirmEmailHandler : IRequestHandler<ConfirmEmailRequest, ConfirmEmailResponse>
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
            confirmResponse.Message = "Lo sentimos! email no pudo ser confirmado";
            return confirmResponse;
        }
        confirmResponse.Message = "Excelente! email ha sido confirmado";
        confirmResponse.Transaction = true;
        return confirmResponse;
    }
}
