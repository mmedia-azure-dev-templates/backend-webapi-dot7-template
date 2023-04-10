using Boilerplate.Application.Features.Articles.ArticleSearch;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class TableComponent : IComponent
{
    public List<ArticleSearchResponse> Model { get; }
    public TableComponent(List<ArticleSearchResponse> model)
    {
        Model = model;
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
                header.Cell().Element(CellStyle).Text("#");
                //header.Cell().Element(CellStyle).Text("Sku");
                header.Cell().Element(CellStyle).Text("Producto");
                header.Cell().Element(CellStyle).AlignCenter().Text("Precio unitario");
                header.Cell().Element(CellStyle).AlignRight().Text("Cantidad");
                header.Cell().Element(CellStyle).AlignRight().Text("Total");

                static IContainer CellStyle(IContainer container)
                {
                    return container.DefaultTextStyle(x => x.SemiBold()).PaddingVertical(5).BorderBottom(1).BorderColor(Colors.Black);
                }
            });

            // step 3
            foreach (var item in Model)
            {
                table.Cell().Element(CellStyle).Text(Model.IndexOf(item) + 1);
               // table.Cell().Element(CellStyle).Text(item.Sku);
                table.Cell().Element(CellStyle).Text(item.Display);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Cost}$");
                table.Cell().Element(CellStyle).AlignRight().Text(item.Quantity);
                table.Cell().Element(CellStyle).AlignRight().Text($"{item.Total}$");

                static IContainer CellStyle(IContainer container)
                {
                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                }
            }
        });
    }
}
