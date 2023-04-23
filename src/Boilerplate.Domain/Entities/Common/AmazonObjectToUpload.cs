using Microsoft.AspNetCore.Http;

namespace Boilerplate.Domain.Entities.Common;

/// <summary>
/// Amazon Object To Upload
/// </summary>
public class AmazonObjectToUpload
{
    /// <summary>
    /// Prefix
    /// </summary>
    public string? Prefix { get; set; }

    /// <summary>
    /// HTTP file
    /// </summary>
    public IFormFile? FormFile { get; set; }
}
