using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateRequest : IRequest<ArticleCreateResponse>
{
    public int Provider { get; set; } = 1;
    public string Sku { get; set; }
    public string Display { get; set; }
    public int Brand { get; set; } = 1;
    public string? Notes { get; set; }
    public string? Meta { get; set; }
    public bool Discontinued { get; set; } = false;
    public List<ArticleItemsPrices>? ListArticleItemsPrices { get; set; }
}

public class ArticleItemsPrices
{
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public decimal? Price { get; set; }
}
