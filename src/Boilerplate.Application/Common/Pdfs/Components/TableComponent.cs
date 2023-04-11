using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class TableComponent : IComponent
{
    public Order _order { get; set; }
    public List<ArticleSearchResponse> Model { get; }
    public TableComponent(List<ArticleSearchResponse> model, Order order)
    {
        Model = model;
        _order = order;
    }
    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(25);
                columns.RelativeColumn(3);
                columns.RelativeColumn();
                columns.RelativeColumn();
                columns.RelativeColumn();
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).AlignCenter().Text("#");
                //header.Cell().Element(CellStyle).Text("Sku");
                header.Cell().Element(CellStyle).AlignCenter().Text("Producto");
                header.Cell().Element(CellStyle).AlignCenter().Text("Precio U.");
                header.Cell().Element(CellStyle).AlignCenter().Text("Cantidad");
                header.Cell().Element(CellStyle).AlignCenter().Text("Total");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).AlignMiddle().Border(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).AlignCenter().Text(Model.IndexOf(item) + 1);
               // table.Cell().Element(CellStyle).Text(item.Sku);
                table.Cell().Element(CellStyle).PaddingLeft(4).Text(item.Display);
                table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text($"{item.Cost}");
                table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text(item.Quantity);
                table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text($"{item.Total}");

                static IContainer CellStyle(IContainer container)
                {
                    return container.Border(1).BorderColor(Colors.Black).PaddingVertical(5);
                }
            }

            table.Footer(footer =>
            {
                footer.Cell().Row(1).Column(4).Element(CellStyle).PaddingRight(4).AlignRight().Text("Subtotal: ").SemiBold();
                footer.Cell().Row(2).Column(4).Element(CellStyle).PaddingRight(4).AlignRight().Text("IVA 12%: ").SemiBold();
                footer.Cell().Row(3).Column(4).Element(CellStyle).PaddingRight(4).AlignRight().Text("Total: ").SemiBold();
                footer.Cell().Row(1).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text($"${_order.SubTotal}");
                footer.Cell().Row(2).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                footer.Cell().Row(3).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text($"${_order.Total}").SemiBold();
                static IContainer CellStyle(IContainer container)
                {
                    return container.AlignMiddle().MinHeight(20).Border(1).BorderColor(Colors.Black);
                }

            });
        });
    }
}
