using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemUpdateBySku;
public class ArticleItemUpdateBySkuRequest : IRequest<ArticleItemUpdateBySkuResponse>
{
    public string Sku { get; set; }
    public string Display { get; set; }
    public PaymentMethodId PaymentMethodId { get; set; }
    public decimal Price { get; set; }
}
