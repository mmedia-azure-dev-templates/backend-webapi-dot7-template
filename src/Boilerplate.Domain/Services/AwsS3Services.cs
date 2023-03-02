using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;
using ISession = Boilerplate.Domain.Implementations.ISession;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Services;
public class AwsS3Service : IAwsS3Service
{
    //https://stackoverflow.com/questions/61919704/get-url-of-uploaded-s3-object-net
    private readonly IAmazonS3 _s3Client;
    private readonly IAwsS3Configuration _awsS3Configuration;
    private readonly ISession _session;

    public AwsS3Service(IAwsS3Configuration awsS3Configuration, ISession session)
    {
        _awsS3Configuration = awsS3Configuration;
        _session = session;
        _awsS3Configuration.BucketFolderRelative = _session.UserId.ToString();
        _s3Client = new AmazonS3Client(_awsS3Configuration.AwsAccessKey, _awsS3Configuration.AwsSecretAccessKey, RegionEndpoint.GetBySystemName(_awsS3Configuration.Region));
        
    }

    public async Task<AmazonObject> UploadFileAsync(IFormFile file)
    {
        var response = new AmazonObject();
        var bucketExists = await AmazonS3Util.DoesS3BucketExistV2Async(_s3Client, _awsS3Configuration.BucketName);
        if (!bucketExists) return response; //return NotFound($"Bucket {_awsS3Configuration.BucketName} does not exist.");


        var request = new PutObjectRequest()
        {
            BucketName = _awsS3Configuration.BucketName,
            Key = string.IsNullOrEmpty(_awsS3Configuration.BucketFolder) ? file.FileName : $"{_awsS3Configuration.BucketFolder?.TrimEnd('/')}/{file.FileName}",
            InputStream = file.OpenReadStream()
        };
        request.Metadata.Add("Content-Type", file.ContentType);
        await _s3Client.PutObjectAsync(request);

        
        return response;
        //return Ok($"File {_awsS3Configuration.BucketFolder}/{file.FileName} uploaded to S3 successfully!");
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

    //public async Task<bool> DeleteFileAsync(string fileName, string versionId = "")
    //{

    //    try
    //    {
    //        DeleteObjectRequest request = new DeleteObjectRequest
    //        {
    //            BucketName = $"{_bucketName}{_bucketFolder}",
    //            Key = fileName
    //        };
    //        if (!string.IsNullOrEmpty(versionId))
    //            request.VersionId = versionId;

    //        await _awsS3Client.DeleteObjectAsync(request);

    //        return true;

    //    }
    //    catch (Exception)
    //    {
    //        throw;
    //    }
    //}

    ///// <summary>
    ///// Upload Amazon Object
    ///// </summary>
    ///// <param name="amazonObjectToUpload">Requested object file</param>
    ///// <param name="binaryFile">Binary file</param>
    ///// <returns></returns>
    //private async Task<AmazonObject> UploadAmazonObjectAsync(AmazonObjectToUpload amazonObjectToUpload, byte[] binaryFile)
    //{
    //    var fileTransferUtility = new TransferUtility(_awsS3Client);

    //    var key = $"{amazonObjectToUpload.Prefix}{amazonObjectToUpload.FormFile.FileName}";
    //    using (MemoryStream memoryStream = new MemoryStream(binaryFile))
    //    {
    //        await fileTransferUtility.UploadAsync(memoryStream, $"{_bucketName}{_bucketFolder}", key);

    //        var retVal = await _awsS3Client.GetObjectAsync($"{_bucketName}{_bucketFolder}", key);

    //        var result = new AmazonObject
    //        {
    //            ETag = retVal.ETag,
    //            BucketName = _bucketName,
    //            Key = retVal.Key,
    //            LastModified = retVal.LastModified,
    //            Owner = null,
    //            Size = binaryFile.Length,
    //            Name = amazonObjectToUpload.FormFile.FileName,
    //            File = true,
    //            Prefix = amazonObjectToUpload.Prefix,
    //            ObjectUrl = $"https://{_bucketName}.s3.us-east-2.amazonaws.com{_bucketFolder}/{amazonObjectToUpload.Prefix}{amazonObjectToUpload.FormFile.FileName}",
    //            S3Uri = $"s3://{_bucketFolder}{_bucketName}/{amazonObjectToUpload.Prefix}{amazonObjectToUpload.FormFile.FileName}"
    //        };

    //        return result;
    //    }
    //}


    ///// <summary>
    ///// Upload File to Amazon Web Service
    ///// </summary>
    ///// <param name="fileName">Name of file to upload</param>
    ///// <param name="prefix">Spcific prefix</param>
    ///// <param name="binaryFile">Array of binary data</param>
    ///// <returns></returns>
    //public async Task<AmazonObject> UploadFileAmazonAsync(string fileName, string prefix, byte[] binaryFile)
    //{
    //    var fileTransferUtility = new TransferUtility(_awsS3Client);

    //    var key = $"{prefix}{fileName}";
    //    using (MemoryStream memoryStream = new MemoryStream(binaryFile))
    //    {
    //        await fileTransferUtility.UploadAsync(memoryStream, $"{_bucketName}{_bucketFolder}", key);

    //        var retVal = await _awsS3Client.GetObjectAsync($"{_bucketName}{_bucketFolder}", key);

    //        var result = new AmazonObject
    //        {
    //            ETag = retVal.ETag,
    //            BucketName = _bucketName,
    //            Key = retVal.Key,
    //            LastModified = retVal.LastModified,
    //            Owner = null,
    //            Size = binaryFile.Length,
    //            Name = fileName,
    //            File = true,
    //            Prefix = prefix,
    //            ObjectUrl = $"https://{_bucketName}.s3.us-east-2.amazonaws.com{_bucketFolder}/{prefix}{fileName}",
    //            S3Uri = $"s3://{_bucketFolder}{_bucketName}/{prefix}{fileName}"
    //        };

    //        return result;
    //    }
    //}
    //public async Task<byte[]> GetFileAmazonAsyc(string filename)
    //{
    //    try
    //    {
    //        GetObjectRequest getObjectRequest = new GetObjectRequest();
    //        getObjectRequest.BucketName = _bucketName;
    //        getObjectRequest.Key = filename;
    //        byte[] binaryFile = null;
    //        GetObjectResponse response = await _awsS3Client.GetObjectAsync(getObjectRequest);
    //        using (Stream responseStream = response.ResponseStream)
    //        {

    //            byte[] buffer = new byte[16 * 1024];
    //            using (MemoryStream ms = new MemoryStream())
    //            {
    //                int read;
    //                while ((read = responseStream.Read(buffer, 0, buffer.Length)) > 0)
    //                {
    //                    ms.Write(buffer, 0, read);
    //                }
    //                binaryFile = ms.ToArray();

    //                return binaryFile;
    //            }
    //        }
    //        return binaryFile;
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    };

    //}
}
