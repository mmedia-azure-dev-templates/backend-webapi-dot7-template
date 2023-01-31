using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Threading.Tasks;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using AuthPermissions.AspNetCore.Services;
using AuthPermissions;
using Boilerplate.Domain.PermissionsCode;
using AuthPermissions.BaseCode;
using AuthPermissions.AspNetCore;
using RunMethodsSequentially;
using AuthPermissions.AspNetCore.StartupServices;
using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Boilerplate.Infrastructure.Reverse;
using Boilerplate.Api.Extends;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.AdminCode;

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
                    ClockSkew = TimeSpan.Zero //The default is 5 minutes, but we want a quick expires
                };
            });
        //thanks to: https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-5-with-jwt-and-swagger/
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Example2.WebApiWithToken.IndividualAccounts", Version = "v1" });

            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
                };

            c.AddSecurityRequirement(securityRequirement);
        });

        services.RegisterAuthPermissions<Example2Permissions>(options =>
        {
            //This tells AuthP that you don't have multiple instances of your app running,
            //so it can run the startup services without a global lock
            options.UseLocksToUpdateGlobalResources = false;

            //This sets up the JWT Token. The config is suitable for using the Refresh Token with your JWT Token
            options.ConfigureAuthPJwtToken = new AuthPJwtConfiguration
            {
                Issuer = jwtData.Issuer,
                Audience = jwtData.Audience,
                SigningKey = jwtData.SigningKey,
                TokenExpires = new TimeSpan(0, 5, 0), //Quick Token expiration because we use a refresh token
                RefreshTokenExpires = new TimeSpan(1, 0, 0, 0) //Refresh token is valid for one day
            };
        })
        .UsingEfCoreSqlServer(configuration.GetConnectionString("SqlServerConnection")) //NOTE: This uses the same database as the individual accounts DB
        .IndividualAccountsAuthentication<ApplicationUser>()
        .AddSuperUserToIndividualAccounts<ApplicationUser>()
        .RegisterFindUserInfoService<ExtendIndividualAccountUserLookup>()
        .AddRolesPermissionsIfEmpty(AppAuthSetupData.RolesDefinition)
        .AddAuthUsersIfEmpty(AppAuthSetupData.UsersRolesDefinition)
        .SetupAspNetCoreAndDatabase(options =>
        {
            options.RegisterServiceToRunInJob<StartupServiceMigrateAnyDbContext<IdentityLocalDbContext>>();
            options.RegisterServiceToRunInJob<ExtendStartupServicesIndividualAccountsAddDemoUsers>();
        });

        return services;
    }
}
