using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using MediatR;

namespace Boilerplate.Application.Features.Articles.GetArticleById;
public record ArticleSearchRequest(string? Sku, string? Abrevia, string? Display, int? Brand) : PaginatedRequest, IRequest<PaginatedList<ArticleSearchResponse>>;
