using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.Reset;
public class ResetPasswordHandler: IRequestHandler<ResetPasswordRequest,ResetPasswordResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private ResetPasswordResponse _resetPasswordResponse;
    private readonly ILocalizationService _localizationService;
    public ResetPasswordHandler(UserManager<ApplicationUser> userManager, IResetPasswordResponse resetPasswordResponse, ILocalizationService localizationService)
    {
        _userManager = userManager;
        _resetPasswordResponse = (ResetPasswordResponse)resetPasswordResponse;
        _localizationService = localizationService;
    }

    public async Task<ResetPasswordResponse> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            _resetPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("EmailNotExist").Value;
            return _resetPasswordResponse;
        }

        var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.Password);
        if (!resetPassResult.Succeeded)
        {
            _resetPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleError").Value;
            return _resetPasswordResponse;
        }
        
        _resetPasswordResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("ResetPasswordResponseTitleSuccess").Value;
        _resetPasswordResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
        _resetPasswordResponse.Transaction = true;
        return _resetPasswordResponse;
    }
}
