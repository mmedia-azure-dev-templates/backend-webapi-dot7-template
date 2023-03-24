using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
public class PaymentMethodCreateResponse : IPaymentMethodCreateResponse
{
    public string Message { get; set; }
}