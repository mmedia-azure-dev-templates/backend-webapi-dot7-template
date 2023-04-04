using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Orders.OrderByNumber;
public class OrderByNumberRequest : IRequest<OrderByNumberResponse>
{
    public OrderNumber OrderNumber { get; set; }
}