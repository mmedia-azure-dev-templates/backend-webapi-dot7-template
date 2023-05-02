using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemCreateUpdateBySku;
public class ArticleItemUpdateBySkuRequest : IRequest<ArticleItemUpdateBySkuResponse>
{
    public string Sku { get; set; }
    public string Display { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public decimal Price { get; set; }
}
