using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateResponse:IArticleCreateResponse
{
    public Article Article { get; set; }
    public List<ArticleItem> ListArticlesItems { get; set; }
    public string Message { get; set; }
}