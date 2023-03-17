using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public class Article : Entity<ArticleId>, IDataKeyFilterReadWrite
{
    public override ArticleId Id { get; set; }
    public string DataKey { get; set; }

    public int? Provider { get; set; }

    public string? Sku { get; set; }

    public string? Abrevia { get; set; }

    public string? Display { get; set; }

    public decimal Cost { get; set; }

    public int? Brand { get; set; }

    public string? Notes { get; set; }

    public string? Meta { get; set; }

    public bool? Discontinued { get; set; }
}
