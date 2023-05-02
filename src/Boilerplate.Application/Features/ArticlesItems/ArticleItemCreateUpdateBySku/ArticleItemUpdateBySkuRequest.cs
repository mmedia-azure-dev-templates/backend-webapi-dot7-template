using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemCreateUpdateBySku;
public class ArticleItemUpdateBySkuRequest : IRequest<ArticleItemUpdateBySkuResponse>
{
    public required string Sku { get; set; }
    public required string Display { get; set; }
    public required PaymentMethodsType PaymentMethodsType { get; set; }
    public required decimal Price { get; set; }
}
