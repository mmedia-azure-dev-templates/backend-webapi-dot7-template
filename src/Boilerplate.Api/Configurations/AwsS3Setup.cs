using Amazon.S3;
using Boilerplate.Domain.Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Boilerplate.Api.Configurations;

public static class AwsS3Setup
{

    public static IServiceCollection AddAwsS3Setup(this IServiceCollection services, IConfiguration configuration)
    {
        var options = configuration.GetAWSOptions();
        services.AddDefaultAWSOptions(options);
        services.AddAWSService<IAmazonS3>();
        return services;

    }
    //public AwsS3Setup(IAppConfigurationAccessor configurationAccessor)
    //{
    //    _appConfiguration = configurationAccessor.Configuration;

    //    BucketName = _appConfiguration["Aws:BucketName"];
    //    BucketFolder = _appConfiguration["Aws:Folder"];
    //    Region = _appConfiguration["Aws:Region"];
    //    AwsAccessKey = _appConfiguration["Aws:AwsAccessKey"];
    //    AwsSecretAccessKey = _appConfiguration["Aws:AwsSecretAccessKey"];
    //    AwsSessionToken = "";
    //}

    //public string BucketName { get; set; }
    //public string BucketFolder { get; set; }
    //public string Region { get; set; }
    //public string AwsAccessKey { get; set; }
    //public string AwsSecretAccessKey { get; set; }
    //public string AwsSessionToken { get; set; }

}
