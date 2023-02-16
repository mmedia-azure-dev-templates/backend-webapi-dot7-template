using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Boilerplate.Api.Common;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class SwaggerAuthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public SwaggerAuthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        //Add your condition
        if (httpContext.Request.Path.StartsWithSegments("/swagger")
            && !httpContext.User.Identity.IsAuthenticated)
        {
            httpContext.Response.Redirect("/Identity/Account/Login");

            return Task.CompletedTask;
        }
        
        return _next(httpContext);
    }
}

// Extension method used to add the middleware to the HTTP request pipeline.
public static class SwaggerAuthorizedMiddlewareExtensions
{
    public static IApplicationBuilder UseSwaggerAuthorizedMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SwaggerAuthorizedMiddleware>();
    }
}
