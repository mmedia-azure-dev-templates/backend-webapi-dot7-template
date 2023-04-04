using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdRequest : IRequest<OrderByIdResponse>
{
    public OrderId OrderId { get; set; }
}
