using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.PaymentMethods.PaymentMethodUpdate;
public class PaymentMethodUpdateResponse
{
    public string Message { get; set; } = "Método de pago no actualizado";
    public bool Success { get; set; } = false;
}