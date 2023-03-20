using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.OrderItems.OrderItemById;
public class OrderItemByIdRequest : IRequest<OrderItemByIdResponse>
{
    public OrderItemId OrderItemId { get; set; }
}