using MediatR;

namespace Boilerplate.Application.Features.Articles.ArticleAvailable;
public record ArticleAvailableRequest(string Sku) : IRequest<ArticleAvailableResponse>;