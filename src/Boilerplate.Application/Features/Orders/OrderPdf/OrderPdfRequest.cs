using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfRequest : IRequest<OrderPdfResponse>
{
    public OrderId OrderId { get; set; }
}
