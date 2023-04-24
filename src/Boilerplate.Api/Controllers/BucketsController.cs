using Amazon;
using Amazon.S3;
using Amazon.S3.Util;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[Route("api/buckets")]
[ApiController]
[Authorize]
public class BucketsController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAwsS3Configuration _awsS3Configuration;
    public BucketsController(IAwsS3Configuration awsS3Configuration)
    {
        _awsS3Configuration = awsS3Configuration;
        _s3Client = new AmazonS3Client(_awsS3Configuration.AwsAccessKey, _awsS3Configuration.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(_awsS3Configuration.Region));
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateBucketAsync(string bucketName)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        if (bucketExists) return BadRequest($"Bucket {bucketName} already exists.");
        await _s3Client.PutBucketAsync(bucketName);
        return Ok($"Bucket {bucketName} created.");
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllBucketAsync()
    {
        var data = await _s3Client.ListBucketsAsync();
        var buckets = data.Buckets.Select(b => { return b.BucketName; });
        return Ok(buckets);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteBucketAsync(string bucketName)
    {
        await _s3Client.DeleteBucketAsync(bucketName);
        return NoContent();
    }

}
