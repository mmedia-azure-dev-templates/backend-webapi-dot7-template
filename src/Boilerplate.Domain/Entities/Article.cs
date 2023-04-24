using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Articles", Schema = "web")]
public class Article : Entity<ArticleId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override ArticleId Id { get; set; }
    public string DataKey { get; set; } = null!;
    public int? Provider { get; set; }
    public string Sku { get; set; } = null!;
    public string? Abrevia { get; set; }
    public string Display { get; set; } = null!;
    public decimal Cost { get; set; }
    public int? Brand { get; set; }
    public string? Notes { get; set; }
    public string? Meta { get; set; }
    public bool? Discontinued { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
