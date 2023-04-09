using Amazon.S3;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Domain;

public static class DomainServicesRegistration
{
    public static IServiceCollection ConfigureDomainServices(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonS3>();
        services.AddSingleton<IAwsS3Configuration, AwsS3ConfigurationServices>();
        services.AddScoped<IAwsS3Service, AwsS3Service>();

        return services;
    }
}
