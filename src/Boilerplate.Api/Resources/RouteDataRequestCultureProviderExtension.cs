using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace Boilerplate.Api.Resources;

public class CustomRouteDataRequestCultureProvider : QueryStringRequestCultureProvider
{
    //public int IndexOfCulture;
    //public int IndexofUICulture;

    //public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
    //{
    //    if (httpContext == null)
    //        throw new ArgumentNullException(nameof(httpContext));

    //    string culture = null;
    //    string uiCulture = null;

    //    culture = uiCulture = httpContext.Request.Path.Value.Split('/')[IndexOfCulture]?.ToString();

    //    var providerResultCulture = new ProviderCultureResult(culture, uiCulture);

    //    return Task.FromResult(providerResultCulture);
    //}
}
