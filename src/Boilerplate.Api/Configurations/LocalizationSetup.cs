using Boilerplate.Api.Resources;
using LocalizeMessagesAndErrors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;

namespace Boilerplate.Api.Configurations;

public static class LozalizationSetup
{
    public static IServiceCollection AddLocalizationSetup(this IServiceCollection services)
    {

        //Register the SimpleLocalizer with its own Resource file
        //This is used for localization of simple messages
        services.RegisterSimpleLocalizer<SharedResource>();

        services.AddLocalization(options => options.ResourcesPath = "Resources");

        //see https://learn.microsoft.com/es-us/aspnet/core/fundamestals/localization#localization-middleware
        var supportedCultures = new[] { "es", "en" };
        var localizationOptions = new RequestLocalizationOptions()
            .SetDefaultCulture(supportedCultures[0])
            .AddSupportedCultures(supportedCultures)
            .AddSupportedUICultures(supportedCultures);


        //This defines that the culture is selected by the culture cookie
        localizationOptions.RequestCultureProviders = new List<IRequestCultureProvider>()
        {
            new CookieRequestCultureProvider(),
            //new AcceptLanguageHeaderRequestCultureProvider(),
            //new QueryStringRequestCultureProvider()
        };

        services.Configure<RequestLocalizationOptions>(options =>
        {
            List<CultureInfo> supportedCultures = new List<CultureInfo>
            {
                new ("es"),
                new ("en"),
            };
            options.DefaultRequestCulture = new RequestCulture("es");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders = new List<IRequestCultureProvider>()
            {
                new CookieRequestCultureProvider(),
                new AcceptLanguageHeaderRequestCultureProvider(),
                new QueryStringRequestCultureProvider()
            };
        });

        return services;
    }

    public static IApplicationBuilder UseLocalizationSetup(this IApplicationBuilder app)
    {
        var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(localizationOptions.Value);
        return app;
    }
}