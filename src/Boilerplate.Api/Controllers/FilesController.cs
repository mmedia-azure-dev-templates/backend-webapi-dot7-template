using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using ISession = Boilerplate.Domain.Implementations.ISession;

namespace Boilerplate.Api.Controllers;

[Route("api/files")]
[ApiController]
[Authorize]
public class FilesController : ControllerBase
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAwsS3Service _awsS3Service;
    private readonly ISession _session;
    public FilesController(IAmazonS3 s3Client, IAwsS3Service awsS3Service,ISession session)
    {
        _s3Client = s3Client;
        _awsS3Service = awsS3Service;
        _session = session;
    }

    [HttpPost("imageprofilebase64")]
    public async Task<AmazonObject> ImgeProfileBase64(string base64File)
    {
        string bucketFolderRelative = "users/" + _session.UserId.ToString();
        return await _awsS3Service.UploadFileBase64Async(base64File, bucketFolderRelative, "fotoperfil.jpg");
    }

    [HttpPost("imageprofile")]
    public async Task<AmazonObject> ImgeProfile(IFormFile file)
    {
        string bucketFolderRelative = "users/"+_session.UserId.ToString();
        return await _awsS3Service.UploadFileAsync(file, bucketFolderRelative, "fotoperfil.jpg");
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFileAsync(IFormFile file, string bucketName, string? prefix)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
        var request = new PutObjectRequest()
        {
            BucketName = bucketName,
            Key = string.IsNullOrEmpty(prefix) ? file.FileName : $"{prefix?.TrimEnd('/')}/{file.FileName}",
            InputStream = file.OpenReadStream()
        };
        request.Metadata.Add("Content-Type", file.ContentType);
        await _s3Client.PutObjectAsync(request);
        
           
        return Ok($"File {prefix}/{file.FileName} uploaded to S3 successfully!");
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllFilesAsync(string bucketName, string? prefix)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
        var request = new ListObjectsV2Request()
        {
            BucketName = bucketName,
            Prefix = prefix
        };
        var result = await _s3Client.ListObjectsV2Async(request);
        var s3Objects = result.S3Objects.Select(s =>
        {
            var urlRequest = new GetPreSignedUrlRequest()
            {
                BucketName = bucketName,
                Key = s.Key,
                Expires = DateTime.UtcNow.AddMinutes(1)
            };
            return new S3ObjectDto()
            {
                Name = s.Key.ToString(),
                PresignedUrl = _s3Client.GetPreSignedURL(urlRequest),
            };
        });

        return Ok(s3Objects);
    }

    [HttpGet("get-by-key")]
    public async Task<IActionResult> GetFileByKeyAsync(string bucketName, string key)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist.");
        var s3Object = await _s3Client.GetObjectAsync(bucketName, key);
        return File(s3Object.ResponseStream, s3Object.Headers.ContentType);
    }

    [HttpDelete("deletefile")]
    public async Task<IActionResult> DeleteFileAsync(string bucketName, string key)
    {
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, bucketName);
        if (!bucketExists) return NotFound($"Bucket {bucketName} does not exist");
        await _s3Client.DeleteObjectAsync(bucketName, key);
        return NoContent();
    }

    [HttpDelete("deletepath")]
    public async Task<DeleteObjectsResponse> DeletePathAsync(string bucketFolderRelative)
    {
        return await _awsS3Service.DeletePathAsync(bucketFolderRelative);
    }
}
