using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Boilerplate.Api.Common;

public static class LastLoginIpBuilderExtensions
{
    internal const string AuthenticationMiddlewareSetKey = "__AuthenticationMiddlewareSet";

    /// <summary>
    /// Adds the <see cref="AuthenticationMiddleware"/> to the specified <see cref="IApplicationBuilder"/>, which enables authentication capabilities.
    /// </summary>
    /// <param name="app">The <see cref="IApplicationBuilder"/> to add the middleware to.</param>
    /// <returns>A reference to this instance after the operation has completed.</returns>
    public static IApplicationBuilder UseLastLoginIp(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }

        //if (context.User.Identity.IsAuthenticated)
        //{
        //    var userName = context.User.Identity.Name;
        //    //retrieve uer by userName
        //    using (var dbContext = context.RequestServices.GetRequiredService<ApplicationDbContext>())
        //    {
        //        var user = dbContext.ApplicationUser.Where(u => u.UserName == userName).FirstOrDefault();
        //        user.LastLogin = DateTime.Now;
        //        dbContext.Update(user);
        //        dbContext.SaveChanges();
        //    }
        //}

        app.Properties[AuthenticationMiddlewareSetKey] = true;
        return app.UseMiddleware<AuthenticationMiddleware>();
        
        //app.Use(async (context, next) => {
        //    await next.Invoke();
        //    //handle response
        //    //you may also need to check the request path to check whether it requests image
            
        //});
    }
}