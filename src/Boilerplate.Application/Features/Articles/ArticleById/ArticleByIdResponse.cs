using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.Graph;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleById;
public class ArticleByIdResponse
{
    public Article Article { get; set; }
    public List<ArticleItem> ListArticleItem { get; set; }
    public List<PaymentMethod> ListPaymentMethod { get; set; }
}
