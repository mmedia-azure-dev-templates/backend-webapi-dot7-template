using Boilerplate.Application.Common.Behaviors;
using Boilerplate.Application.Common.Handlers;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Boilerplate.Application.MappingProfiles;

namespace Boilerplate.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Boilerplate.Application.IAssemblyMarker).Assembly));
        //services.AddMediatR(typeof(Boilerplate.Application.IAssemblyMarker).GetTypeInfo().Assembly);
        services.AddScoped<INotificationHandler<ValidationError>, ValidationErrorHandler>();
        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}
