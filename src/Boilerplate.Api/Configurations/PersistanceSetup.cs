using Boilerplate.Application.Auth;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class PersistanceSetup
{
    public static IServiceCollection AddPersistenceSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ISession, Session>();
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"),conf =>
            {
                conf.UseHierarchyId();
            });
        });



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
        return services;
    }
}