using Boilerplate.Application.Auth;
using Boilerplate.Domain.Auth.Interfaces;
using Boilerplate.Domain.Entities;
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
        services.AddDbContext<IdentityLocalDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
        });
        services.AddDefaultIdentity<ApplicationUser>(
        options => options.SignIn.RequireConfirmedAccount = false)
        .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<ApplicationUser>>()
        .AddEntityFrameworkStores<IdentityLocalDbContext>()
        .AddDefaultTokenProviders();
        return services;
    }
}