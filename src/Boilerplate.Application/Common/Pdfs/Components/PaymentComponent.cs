using Boilerplate.Application.Features.Payments.PaymentById;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class PaymentComponent : IComponent
{
    private List<PaymentByIdResponse> _paymentByIdResponse { get; }

    public PaymentComponent(List<PaymentByIdResponse> paymentByIdResponse)
    {
        _paymentByIdResponse = paymentByIdResponse;
    }

    public void Compose(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text(text =>
            {
                text.Span("Formas de Pago: ").Bold();
                text.EmptyLine();

                if (_paymentByIdResponse.Count > 0)
                {
                    foreach (var item in _paymentByIdResponse)
                    {
                        text.Span($"| ").ExtraBlack();
                        text.Span($"{item.Display}");
                        text.Span($" | ").ExtraBlack();
                    }
                }
            });
        });
    }
}