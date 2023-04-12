using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;

namespace Boilerplate.Application.Features.Orders.OrderSearch;

public class OrderSearchRequest : PaginatedRequest, IRequest<PaginatedList<OrderSearchResponse>>
{
    public DateTimeOffset StartDate { get; set; } 
    public DateTimeOffset EndDate { get; set; }
    public OrderFilterType? OrderFilterType { get; set; }
    public string? Search { get; set; }
}

