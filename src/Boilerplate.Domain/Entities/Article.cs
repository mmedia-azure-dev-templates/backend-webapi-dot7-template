using Boilerplate.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public partial class Article : Entity<ArticleId>
{
    [Required]
    public override ArticleId Id { get; set; }

    public int? Provider { get; set; }

    public string? Sku { get; set; }

    public string? Abrevia { get; set; }

    public string? Name { get; set; }

    public decimal Cost { get; set; }

    public int? Brand { get; set; }

    public string? Notes { get; set; }

    public string? Meta { get; set; }

    public bool? Discontinued { get; set; }
}
