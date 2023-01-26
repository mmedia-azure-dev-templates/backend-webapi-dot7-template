using Boilerplate.Application.Auth;
using Boilerplate.Domain.Auth.Interfaces;
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
            o.UseNpgsql(configuration.GetConnectionString("PostgresConnection"), x => x.MigrationsHistoryTable("__EFMigrationsHistory", "web"));
        });

        services.AddDefaultIdentity<IdentityUser>(
        options => options.SignIn.RequireConfirmedAccount = false)
        .AddEntityFrameworkStores<ApplicationDbContext>();

        return services;
    }
}