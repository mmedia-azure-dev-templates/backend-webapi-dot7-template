using AuthPermissions.AdminCode;
using Boilerplate.Application.Features.Auth.Authenticate;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
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
                    return;
                }
                var user = await _userManager.FindByEmailAsync(userreq.Email);

                if (user == null)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                var hours = Convert.ToInt32((DateTime.Now - user.LastLogin).Value.TotalHours);

                if (hours > 1)
                {
                    user.LastLogin = DateTime.Now;
                    await _userManager.UpdateAsync(user);
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    return;
                }

                var status = await service.FindAuthUserByUserIdAsync(user.Id);

                if (status.IsValid)
                {
                    var isSuperAdmin = status.Result.UserRoles.Where(x => x.RoleName == "SuperAdmin").Count();

                    if (isSuperAdmin < 1)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        return;
                    }
                }

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