using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Payments.PaymentById;
public class PaymentByIdRequest : IRequest<PaymentByIdResponse>
{
    public OrderId OrderId { get; set; }
}
