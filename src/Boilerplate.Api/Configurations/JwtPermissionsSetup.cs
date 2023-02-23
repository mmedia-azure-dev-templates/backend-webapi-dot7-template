using AuthPermissions;
using AuthPermissions.BaseCode;
using AuthPermissions.BaseCode.SetupCode;
using Boilerplate.Api.Extends;
using Boilerplate.Api.Resources;
using Boilerplate.Application.Common;
using Boilerplate.Application.Services;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.PermissionsCode;
using Boilerplate.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Api.Configurations;

public static class JwtPermissionsSetup
{
    public static IServiceCollection AddJwtSetup(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure Authentication using JWT token with refresh capability
        var jwtData = new JwtSetupData();
        configuration.Bind("JwtData", jwtData);

        //The solution to getting the nameidentifier claim to have the user's Id was found in https://stackoverflow.com/a/70315108/1434764
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtData.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtData.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SigningKey)),
                    ClockSkew = TimeSpan.FromMinutes(120) //TimeSpan.Zero //The default is 5 minutes, but we want a quick expires for JTW refresh
                };

                //This code came from https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
                //It returns a useful header if the JWT Token has expired
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });


        var tokenExpire = int.Parse(configuration.GetSection("JwtData:TokenExpire").Value!);

        services.RegisterAuthPermissions<DefaultPermissions>(options =>
        {
            options.UseLocksToUpdateGlobalResources = false;
            options.TenantType = TenantTypes.SingleLevel;

            //This sets up the JWT Token. The config is suitable for using the Refresh Token with your JWT Token
            options.ConfigureAuthPJwtToken = new AuthPJwtConfiguration
            {
                Issuer = jwtData.Issuer,
                Audience = jwtData.Audience,
                SigningKey = jwtData.SigningKey,
                TokenExpires = new TimeSpan(0, tokenExpire, 0), //Quick Token expiration because we use a refresh token
                RefreshTokenExpires = new TimeSpan(1, 0, 0, 0) //Refresh token is valid for one day
            };
        })
        .UsingEfCoreSqlServer(configuration.GetConnectionString("SqlServerConnection")) //NOTE: This uses the same database as the individual accounts DB
        .IndividualAccountsAuthentication<ApplicationUser>()
        .RegisterTenantChangeService<JibanTenantChangeService>()
        .RegisterAddClaimToUser<AddTenantNameClaim>()
        .RegisterAddClaimToUser<AddRefreshEveryMinuteClaim>()
        .RegisterFindUserInfoService<ExtendIndividualAccountUserLookup>()
        //.RegisterAuthenticationProviderReader<SyncIndividualAccountUsers>()
        .SetupAuthPLocalization<SharedResource>(new[] { "es", "en" })
        .SetupAspNetCoreAndDatabase();

        return services;
    }
}
