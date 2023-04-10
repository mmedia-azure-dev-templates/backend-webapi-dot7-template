using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Orders.OrderValid;
public class OrderValidRequest : IRequest<OrderValidResponse>
{
    public OrderId OrderId { get; set; }
}
