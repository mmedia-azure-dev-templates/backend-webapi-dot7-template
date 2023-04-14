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
                page.Margin(20);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
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
            column.Item().Element(ComposeComments);
        });
    }

    void ComposeTable(IContainer container)
    {
        new ProductsComponent(_orderValidResponse.OrderByIdResponse.ArticleSearchResponse, _orderValidResponse.OrderByIdResponse.Order).Compose(container);
    }
    void ComposeComments(IContainer container)
    {
        new NotesComponent(_orderValidResponse.OrderByIdResponse.Order.Notes).Compose(container);
    }

    void ComposeFooter(IContainer container)
    {
        new FooterComponent().Compose(container);
    }
}