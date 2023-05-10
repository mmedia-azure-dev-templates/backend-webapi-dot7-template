using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Domain.Entities;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleById;
public class ArticleByIdResponse
{
    public Article Article { get; set; }
    public List<ArticleItem> ListArticleItem { get; set; }
    public List<PaymentMethodByIdResponse> ListPaymentMethodByIdResponse { get; set; }
}
