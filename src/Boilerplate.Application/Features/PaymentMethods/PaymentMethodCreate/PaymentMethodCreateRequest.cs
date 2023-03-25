using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
public class PaymentMethodCreateRequest : IRequest<PaymentMethodCreateResponse>
{
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public string? Icon { get; set; }
}
