using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
public class PaymentMethodCreateResponse : IPaymentMethodCreateResponse
{
    public string Message { get; set; } = "Método de pago no creado";
    public bool Success { get; set; } = false;
}