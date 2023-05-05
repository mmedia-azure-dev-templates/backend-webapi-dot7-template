using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Articles.ArticleSearch;

public class ArticleSearchRequest : PaginatedRequest, IRequest<PaginatedList<ArticleSearchResponse>>
{
    public DateTimeOffset StartDate { get; set; }
    public DateTimeOffset EndDate { get; set; }
    public OrderFilterType? OrderFilterType { get; set; }
    public string? Search { get; set; }
}