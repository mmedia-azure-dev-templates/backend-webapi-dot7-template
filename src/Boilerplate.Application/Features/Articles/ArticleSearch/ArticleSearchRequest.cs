using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using MediatR;

namespace Boilerplate.Application.Features.Articles.ArticleSearch;

public class ArticleSearchRequest : PaginatedRequest, IRequest<PaginatedList<ArticleSearchResponse>>
{
    public string? Sku { get; set; }
    public string? Abrevia { get; set; }
    public string? Display { get; set; }
    public int? Brand { get; set; }
}

