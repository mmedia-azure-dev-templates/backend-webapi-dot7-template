using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Orders.OrderSearch;

public class OrderSearchRequest : PaginatedRequest, IRequest<PaginatedList<OrderSearchResponse>>
{
    public OrderNumber? OrderNumber { get; set; }
    public string? Abrevia { get; set; }
    public string? Display { get; set; }
    public int? Brand { get; set; }
}

