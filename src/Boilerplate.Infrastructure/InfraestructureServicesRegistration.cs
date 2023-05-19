using AuthPermissions;
using AuthPermissions.BaseCode;
using AuthPermissions.BaseCode.SetupCode;
using AuthPermissions.SupportCode.AddUsersServices;
using AuthPermissions.SupportCode.AddUsersServices.Authentication;
using Boilerplate.Application.Common;
using Boilerplate.Application.Services;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.PermissionsCode;
using Boilerplate.Infrastructure.Configuration;
using Boilerplate.Infrastructure.Context;
using Boilerplate.Infrastructure.Extends;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure;

public static class InfraestructureServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<ApplicationUser>(
        options => {
            options.SignIn.RequireConfirmedEmail = true;
            options.SignIn.RequireConfirmedPhoneNumber = false;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser>>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>(); // add this line
            o.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), conf =>
            {
                conf.CommandTimeout(180);
                conf.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                conf.UseHierarchyId();
            });
        });

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


        var tokenExpire = int.Parse(configuration["JwtData:TokenExpire"]!);

        services.RegisterAuthPermissions<DefaultPermissions>(options =>
        {
            options.UseLocksToUpdateGlobalResources = false;
            options.TenantType = TenantTypes.SingleLevel;
            options.LinkToTenantType = LinkToTenantTypes.OnlyAppUsers;
            options.EncryptionKey = "sadafdwesEEEED1rxsaASV";

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
        .RegisterAuthenticationProviderReader<ExtendSyncIndividualAccountUsers>()
        .SetupAspNetCoreAndDatabase();

        //manually add services from the AuthPermissions.SupportCode project
        //Add the SupportCode services
        services.AddScoped<IContext, ApplicationDbContext>();
        services.AddTransient<IAddNewUserManager, ExtendIndividualUserAddUserManager<ApplicationUser>>();
        services.AddTransient<ISignInAndCreateTenant, SignInAndCreateTenant>();
        services.AddTransient<IInviteNewUserService, ExtendInviteNewUserService>();
        services.AddLocalizationSetup();
        return services;
    }
}
