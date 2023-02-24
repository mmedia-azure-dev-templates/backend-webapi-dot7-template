using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.Forgot;
public record ForgotPasswordResponse
{
    private readonly ILocalizationService _localizationService;
    public string Text { get; set; } 
    public string Title { get; set; }
    public string Icon { get; set; }
    public bool Transaction { get; set; } = false!;

    public ForgotPasswordResponse(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        Text = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTextError").Value;
        Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleError").Value;
        Icon = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconError").Value;
    }
    
}
