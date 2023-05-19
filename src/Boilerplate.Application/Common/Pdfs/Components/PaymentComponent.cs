using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class PaymentComponent : IComponent
{
    private PaymentMethodByIdResponse _paymentMethodByIdResponse { get; }

    public PaymentComponent(PaymentMethodByIdResponse paymentMethodByIdResponse)
    {
        _paymentMethodByIdResponse = paymentMethodByIdResponse;
    }

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text(text =>
            {
                text.Span("Formas de Pago: ").Bold();
                text.EmptyLine();
                text.Span($"| ").ExtraBlack();
                text.Span($"{_paymentMethodByIdResponse?.Display}");
                text.Span($" | ").ExtraBlack();
            });
        });
    }
}