using Microsoft.AspNetCore.Http;

namespace Boilerplate.Domain.Entities.Common;

/// <summary>
/// Amazon Object To Upload in Documents
/// </summary>
public class DocumentAmazonObjectToUpload
{
    /// <summary>
    /// File Name
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// File
    /// </summary>
    public IFormFile? File { get; set; }

    /// <summary>
    /// Prefix
    /// </summary>
    public string? Prefix { get; set; }
}
