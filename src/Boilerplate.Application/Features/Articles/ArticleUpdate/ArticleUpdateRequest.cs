using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleUpdate;
public class ArticleUpdateRequest : IRequest<ArticleUpdateResponse>
{
    public ArticleId ArticleId { get; set; }
    public int Provider { get; set; } = 1;
    public string Sku { get; set; }
    public string Display { get; set; }
    public int Brand { get; set; } = 1;
    public string? Notes { get; set; }
    public string? Meta { get; set; }
    public bool Discontinued { get; set; } = false;
    public List<ArticleItemsPrices> ListArticleItemsPrices { get; set; } = new();
}
