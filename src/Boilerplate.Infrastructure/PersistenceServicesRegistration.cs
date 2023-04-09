using Boilerplate.Domain.Entities.Common;
using Boilerplate.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Infrastructure;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(o =>
        {
            o.ReplaceService<IValueConverterSelector, StronglyTypedIdValueConverterSelector>(); // add this line
            o.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"), conf =>
            {
                conf.UseHierarchyId();
            });
        });

        return services;
    }
}
