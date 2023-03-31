using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using ISession = Boilerplate.Domain.Implementations.ISession;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web;
using System.Net;
using Azure.Core;
using System.Collections.Generic;

namespace Boilerplate.Domain.Services;
public class AwsS3Service : IAwsS3Service
{
    //https://stackoverflow.com/questions/61919704/get-url-of-uploaded-s3-object-net
    private readonly IAmazonS3 _s3Client;
    private readonly IAwsS3Configuration _awsS3Configuration;

    public AwsS3Service(IAwsS3Configuration awsS3Configuration)
    {
        _awsS3Configuration = awsS3Configuration;
        _s3Client = new AmazonS3Client(_awsS3Configuration.AwsAccessKey, _awsS3Configuration.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(_awsS3Configuration.Region));
        
    }

    public async Task<AmazonObject> UploadFileAsync(IFormFile file, string bucketFolderRelative,string fileName)
    {
        var response = new AmazonObject();
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsS3Configuration.BucketName);
        if (!bucketExists) return response; //return NotFound($"Bucket {_awsS3Configuration.BucketName} does not exist.");


        var request = new PutObjectRequest()
        {
            BucketName = $"{_awsS3Configuration.BucketName}",
            Key = $"{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}", //$"{file.FileName}",
            InputStream = file.OpenReadStream()
        };
        request.Metadata.Add("Content-Type", file.ContentType);
        await _s3Client.PutObjectAsync(request);
        // ObjectUrl
        //https://mad-storage.s3.amazonaws.com/devempresas/users/a49cf027-915b-4933-9fce-30def6d67037/fotoperfil.jpg
        response.ObjectUrl = $"https://{_awsS3Configuration.BucketName}.s3.amazonaws.com/{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}";
        // S3Uri
        //https://mad-storage.s3.amazonaws.com/devempresas/users/a49cf027-915b-4933-9fce-30def6d67037/fotoperfil.jpg
        response.S3Uri = $"s3://{_awsS3Configuration.BucketName}/{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}";
        return response;
    }

    public async Task<AmazonObject> UploadFileBase64Async(string base64File, string bucketFolderRelative, string fileName)
    {
        var response = new AmazonObject();
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsS3Configuration.BucketName);
        if (!bucketExists) return response; //return NotFound($"Bucket {_awsS3Configuration.BucketName} does not exist.");
        Regex regex = new Regex(@"^[\w/\:.-]+;base64,");
        base64File = regex.Replace(base64File, string.Empty);
        using (var inputStream = new MemoryStream(Convert.FromBase64String(base64File)))
        {
            var request = new PutObjectRequest()
            {
                InputStream = inputStream,
                //ContentType = "application/pdf",
                BucketName = $"{_awsS3Configuration.BucketName}",
                Key = $"{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}",
                CannedACL = S3CannedACL.BucketOwnerFullControl
            };
            await _s3Client.PutObjectAsync(request);
        }
        // ObjectUrl
        //https://mad-storage.s3.amazonaws.com/devempresas/users/a49cf027-915b-4933-9fce-30def6d67037/fotoperfil.jpg
        response.ObjectUrl = $"https://{_awsS3Configuration.BucketName}.s3.amazonaws.com/{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}";
        // S3Uri
        //https://mad-storage.s3.amazonaws.com/devempresas/users/a49cf027-915b-4933-9fce-30def6d67037/fotoperfil.jpg
        response.S3Uri = $"s3://{_awsS3Configuration.BucketName}/{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}";
        return response;
    }

    //public async Task<AmazonObject> UploadFileAsync(AmazonObjectToUpload amazonObjectToUpload)
    //{
    //    try
    //    {
    //        using (var newMemoryStream = new MemoryStream())
    //        {
    //            amazonObjectToUpload.FormFile.CopyTo(newMemoryStream);
    //            AmazonObject awsResponse = await UploadAmazonObjectAsync(amazonObjectToUpload, newMemoryStream.ToArray());
    //            return awsResponse;
    //        }

    //    }
    //    catch (Exception e)
    //    {
    //        //Logger.Error(e.Message, e);
    //        return null;
    //    }
    //}

    public async Task<DeleteObjectResponse> DeleteFileAsync(string bucketFolderRelative, string fileName)
    {
        var request = new DeleteObjectRequest()
        {
            BucketName = $"{_awsS3Configuration.BucketName}",
            Key = $"{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}/{fileName}",
        };
        return await _s3Client.DeleteObjectAsync(request);
    }

    public async Task<DeleteObjectsResponse> DeletePathAsync(string bucketFolderRelative)
    {
        DeleteObjectsResponse deleteObjectsResponse = new DeleteObjectsResponse();
        var request = new ListObjectsRequest
        {
            BucketName = $"{_awsS3Configuration.BucketName}",
            Prefix = $"{_awsS3Configuration.BucketFolder}/{bucketFolderRelative}",
        };

        var response = await _s3Client.ListObjectsAsync(request);
        var keys = new List<KeyVersion>();
        foreach (var item in response.S3Objects)
        {
            // Here you can provide VersionId as well.
            keys.Add(new KeyVersion { Key = item.Key });
        }

        var multiObjectDeleteRequest = new DeleteObjectsRequest()
        {
            BucketName = $"{_awsS3Configuration.BucketName}",
            Objects = keys
        };

        if(multiObjectDeleteRequest.Objects.Count == 0)
        {
            return deleteObjectsResponse;
        }

        return  await _s3Client.DeleteObjectsAsync(multiObjectDeleteRequest);
    }
}
