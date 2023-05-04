using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleSearch;
public class ArticleSearchResponse
{
    public Article Article { get; set; }
    public List<ArticleItemPaymentMethodResponse> ListArticleItemPaymentMethodResponse { get; set; }
}

public class ArticleItemPaymentMethodResponse
{
    public ArticleItemId ArticleItemId { get; set; }
    public PaymentMethodId PaymentMethodId { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string Display { get; set; }
    public decimal Price { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
