using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;

namespace Boilerplate.Domain.Entities.Common;
public class SweetAlert: ISweetAlert
{
    private readonly ILocalizationService _localizationService;
    public string Text { get; set; }
    public string Title { get; set; }
    public SweetAlertIconType Icon { get; set; }

    public SweetAlert(ILocalizationService localizationService)
    {
        _localizationService = localizationService;
        Text = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTextError").Value;
        Title = _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseTitleError").Value;
        Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconError").Value);
    }
}
