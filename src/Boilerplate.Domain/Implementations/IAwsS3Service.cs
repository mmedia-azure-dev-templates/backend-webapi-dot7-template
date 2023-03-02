using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IAwsS3Service
{
    public Task<AmazonObject> UploadFileAsync(IFormFile file);
}
