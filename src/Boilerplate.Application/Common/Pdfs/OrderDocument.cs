using Boilerplate.Application.Common.Pdfs.Components;
using Boilerplate.Application.Features.Orders.OrderValid;
using Boilerplate.Domain.Implementations;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Boilerplate.Application.Common.Pdfs;
public class OrderDocument : IDocument
{
    public OrderValidResponse _orderValidResponse { get; set; }

    public OrderDocument(OrderValidResponse orderValidResponse)
    {
        _orderValidResponse = orderValidResponse;
    }
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);
                page.Header().Element(ComposeHeader);
                //page.Header().Height(100).Background(Colors.Grey.Lighten1);
                page.Content().Element(ComposeContent); //.Background(Colors.Grey.Lighten3);
                page.Footer().Height(50).Background(Colors.Grey.Lighten1);
            });
    }

    void ComposeHeader(IContainer container)
    {
        new HeaderComponent(_orderValidResponse.OrderByIdResponse).Compose(container);
    }

    void ComposeContent(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);


            column.Item().Element(ComposeTable);

            var totalPrice = _orderValidResponse.OrderByIdResponse.Order.Total;
            column.Item().AlignRight().Text($"Grand total: {totalPrice}$").FontSize(14);

        });
    }

    void ComposeTable(IContainer container)
    {
        new TableComponent(_orderValidResponse.OrderByIdResponse.ArticleSearchResponse).Compose(container);
    }
}