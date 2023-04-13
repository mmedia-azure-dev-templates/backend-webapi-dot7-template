using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleSearch;
public class ArticleSearchResponse
{
    public ArticleId ArticleId { get; set; }
    public OrderId? OrderId { get; set; } = null;
    public bool Delivered { get; set; } = false;
    public int? Provider { get; set; }
    public string? Sku { get; set; }
    public string? Abrevia { get; set; }
    public string? Display { get; set; }
    public int Quantity { get; set; }
    public decimal Cost { get; set; }
    public decimal Total { get; set; } = 0;
    public int? Brand { get; set; }
    public string? Notes { get; set; }
    public string? Meta { get; set; }
    public bool? Discontinued { get; set; }
    public bool IsSelected { get; set; }
}
