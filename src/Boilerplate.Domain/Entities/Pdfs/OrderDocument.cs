using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boilerplate.Domain.Entities.Pdfs;
public class OrderDocument : IDocument
{
    OrderDocumentDataSource _orderDocumentDataSource { get; set; }
    public OrderDocument(OrderDocumentDataSource orderDocumentDataSource)
    {
        _orderDocumentDataSource = orderDocumentDataSource;
    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Height(100).Background(Colors.Grey.Lighten1);
                page.Content().Background(Colors.Grey.Lighten3);
                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
    }
}