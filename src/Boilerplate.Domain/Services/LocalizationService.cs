﻿using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Resources;
using Microsoft.Extensions.Localization;
using System.Reflection;

namespace Boilerplate.Domain.Services;
public class LocalizationService : ILocalizationService
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