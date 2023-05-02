using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateResponse:IArticleCreateResponse
{
    public Article Article { get; set; } 
}