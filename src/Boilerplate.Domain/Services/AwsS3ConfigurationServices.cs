using Boilerplate.Domain.Implementations;
using Microsoft.Extensions.Configuration;

namespace Boilerplate.Domain.Services;

public class AwsS3ConfigurationServices : IAwsS3Configuration
{
    private readonly IConfiguration _configuration;

    public AwsS3ConfigurationServices(IConfiguration configuration)
    {
        _configuration = configuration;
        BucketName = _configuration["AWS_BUCKET"]!;
        BucketFolder = _configuration["AWS_FOLDER"]!;
        BucketFolderRelative = "";
        Region = _configuration["AWS_REGION"]!;
        AwsAccessKey = _configuration["AWS_ACCESS_KEY_ID"]!;
        AwsSecretAccessKey = _configuration["AWS_SECRET_ACCESS_KEY"]!;
        AwsSessionToken = "";
    }

    public string BucketName { get; set; }
    public string BucketFolder { get; set; }
    public string BucketFolderRelative { get; set; }
    public string Region { get; set; }
    public string AwsAccessKey { get; set; }
    public string AwsSecretAccessKey { get; set; }
    public string AwsSessionToken { get; set; }
}
