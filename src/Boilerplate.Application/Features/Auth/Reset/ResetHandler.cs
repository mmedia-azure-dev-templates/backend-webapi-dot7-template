using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.Reset;
public class ResetHandler: IRequestHandler<ResetRequest,ResetResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IMediator _mediator;
    public ResetHandler(UserManager<ApplicationUser> userManager, IMailService mail, IMediator mediator)
    {
        _userManager = userManager;
        _mail = mail;
        _mediator = mediator;
    }

    public async Task<ResetResponse> Handle(ResetRequest request, CancellationToken cancellationToken)
    {
        ResetResponse resetResponse = new ResetResponse();
        request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            resetResponse.Message = "User not found!";
            return resetResponse;
        }
            

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (!resetPassResult.Succeeded)
        {
            resetResponse.Message = "Reset password failed!";
            return resetResponse;
        }

        resetResponse.Message = "Reset password success!";
        resetResponse.Transaction = true;
        return resetResponse;
    }
}
