using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class HeaderComponent : IComponent
{
    public InvoiceModel Model { get; }

    public HeaderComponent(InvoiceModel model)
    {
        Model = model;
    }

    public void Compose(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column.Item().Text($"Invoice #{Model.InvoiceNumber}").Style(titleStyle);

                column.Item().Text(text =>
                {
                    text.Span("Issue date: ").SemiBold();
                    text.Span($"{Model.IssueDate:d}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Due date: ").SemiBold();
                    text.Span($"{Model.DueDate:d}");
                });
            });

            row.ConstantItem(100).Height(50).Placeholder();
        });
    }
}