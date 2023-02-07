using Boilerplate.Application.Services;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class MailSetup
{
    public static IServiceCollection AddMailSetup(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IMailService, MailService>();
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

        return services;
    }
}
