using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Articles.ArticleById;
public class ArticleByIdRequest : IRequest<ArticleByIdResponse>
{
    public ArticleId ArticleId { get; set; }
}
