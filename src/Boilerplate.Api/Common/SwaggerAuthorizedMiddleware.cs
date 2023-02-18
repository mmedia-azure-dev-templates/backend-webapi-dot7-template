using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Net;
using System;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Text;
using Boilerplate.Application.Features.Auth.Authenticate;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Boilerplate.Domain.Entities;
using AuthPermissions.AdminCode;

namespace Boilerplate.Api.Common;
// You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
public class SwaggerAuthorizedMiddleware
{
    private readonly RequestDelegate _next;

    public SwaggerAuthorizedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager, IAuthUsersAdminService service)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            string authHeader = context.Request.Headers["Authorization"];
            if (authHeader != null && authHeader.StartsWith("Basic "))
            {
                // Get the credentials from request header
                var header = AuthenticationHeaderValue.Parse(authHeader);
                var inBytes = Convert.FromBase64String(header.Parameter);
                var credentials = Encoding.UTF8.GetString(inBytes).Split(':');
                string username = credentials[0];
                string password = credentials[1];

                AuthenticateRequest userreq = new()
                {
                    Email = username,
                    Password = password
                };
                //NOTE: The _signInManager.PasswordSignInAsync does not change the current ClaimsPrincipal - that only happens on the next access with the token
                var result = await _signInManager.PasswordSignInAsync(userreq.Email, userreq.Password, false, false);
                if (!result.Succeeded)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
                var user = await _userManager.FindByEmailAsync(userreq.Email);
                var status = await service.FindAuthUserByUserIdAsync(user.Id);
                

                await _next.Invoke(context).ConfigureAwait(false);
                return;
                
            }
            context.Response.Headers["WWW-Authenticate"] = "Basic";
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        }
        else
        {
            await _next.Invoke(context).ConfigureAwait(false);
        }
        ////Add your condition
        //if (httpContext.Request.Path.StartsWithSegments("/swagger")
        //    && !httpContext.User.Identity.IsAuthenticated)
        //{
        //    httpContext.Response.Redirect("/Identity/Account/Login");

        //    return Task.CompletedTask;
        //}
        
        //return _next(httpContext);
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
