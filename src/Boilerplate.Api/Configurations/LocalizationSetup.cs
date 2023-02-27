using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Services;
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
        services.AddLocalization(options => options.ResourcesPath = "Resources");        

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("es-ES"),
                new CultureInfo("en-US"),
                        
            };
            
            options.DefaultRequestCulture = new RequestCulture(culture: supportedCultures[0], uiCulture: supportedCultures[0]);
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            options.RequestCultureProviders = new List<IRequestCultureProvider>()
            {
                //new CookieRequestCultureProvider(),
                //new AcceptLanguageHeaderRequestCultureProvider(),
                new QueryStringRequestCultureProvider(),
                //new CustomRouteDataRequestCultureProvider()
            };
        });

        services.AddScoped<ILocalizationService, LocalizationService>();

        return services;
    }

    public static IApplicationBuilder UseLocalizationSetup(this IApplicationBuilder app)
    {
        var localizationOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
        app.UseRequestLocalization(localizationOptions.Value);
        return app;
    }
}