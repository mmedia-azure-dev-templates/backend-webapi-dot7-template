namespace Boilerplate.Domain.Implementations;

public interface IAwsS3Setup
{
    string AwsAccessKey { get; set; }
    string AwsSecretAccessKey { get; set; }
    string AwsSessionToken { get; set; }
    string BucketName { get; set; }
    string BucketFolder { get; set; }
    string Region { get; set; }
}
