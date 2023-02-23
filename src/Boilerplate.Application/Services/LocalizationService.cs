using Boilerplate.Application.Resources;
using Boilerplate.Domain.Implementations;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Boilerplate.Application.Services;
public class LocalizationService: ILocalizationService
{
    private readonly IStringLocalizer _localizer;

    public LocalizationService(IStringLocalizerFactory factory)
    {
        var type = typeof(SharedResource);
        var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
        _localizer = factory.Create("SharedResource", assemblyName.Name);
    }

    public LocalizedString GetLocalizedHtmlString(string key)
    {
        return _localizer[key];
    }
}