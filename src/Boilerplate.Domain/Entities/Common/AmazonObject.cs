using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boilerplate.Domain.Entities.Common;

/// <summary>
/// Amazon object 
/// </summary>
public class AmazonObject
{
    //
    // Summary:
    //     Any ETag set on the object.
    public string ETag { get; set; }
    //
    // Summary:
    //     The name of the bucket containing this object.
    public string BucketName { get; set; }
    //
    // Summary:
    //     The key of the object.
    public string Key { get; set; }
    //
    // Summary:
    //     The date and time the object was last modified. The date retrieved from S3 is
    //     in ISO8601 format. A GMT formatted date is passed back to the user.
    public DateTime LastModified { get; set; }
    //
    // Summary:
    //     The owner of the object.
    public Owner Owner { get; set; }
    //
    // Summary:
    //     The size of the object.
    public long Size { get; set; }
    public string Name { get; set; }
    public bool File { get; set; }
    public string FileIcon { get; set; }
    public string Prefix { get; set; } = string.Empty;
    public string DestinationKey { get; set; }
    public string ObjectUrl { get; set; }
    public string S3Uri { get; set; }
}
