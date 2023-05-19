using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Application.Features.Payments.PaymentById;
using Boilerplate.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class ProductsComponent : IComponent
{
    public Order _order { get; set; }
    public List<ArticleSearchByPaymentMethodTypeResponse> ArticleSearchResponse { get; }
    public PaymentMethodByIdResponse _paymentMethodByIdResponse { get; }
    public ProductsComponent(Order order,List<ArticleSearchByPaymentMethodTypeResponse> model, PaymentMethodByIdResponse paymentMethodByIdResponse)
    {
        _order = order;
        ArticleSearchResponse = model;
        _paymentMethodByIdResponse = paymentMethodByIdResponse;
    }
    public void Compose(IContainer container)
    {
        container.Table(table =>
        {
            // step 1
            table.ColumnsDefinition(columns =>
            {
                columns.ConstantColumn(20);
                columns.ConstantColumn(80);
                columns.RelativeColumn();
                columns.ConstantColumn(70);
                columns.ConstantColumn(70);
                columns.ConstantColumn(70);
            });

            // step 2
            table.Header(header =>
            {
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("#").SemiBold();
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("Sku").SemiBold();
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("Producto").SemiBold();
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("Precio U.").SemiBold();
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("Cantidad").SemiBold();
                header.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("Total").SemiBold();
            });

            var delivered = TextStyle.Default.BackgroundColor(Colors.Green.Lighten3).FontSize(7).Medium();
            var pending = TextStyle.Default.BackgroundColor(Colors.Orange.Lighten3).FontSize(7).Medium();

            if (ArticleSearchResponse.Count == 0)
            {
                var i = 10;
                for (i=0; i<=8; i++)
                {
                    table.Cell().Element(CellStyle).AlignCenter().Text("");
                    table.Cell().Element(CellStyle).AlignCenter().Text("");
                    table.Cell().Element(CellStyle).PaddingLeft(4).Text("");
                    table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                    table.Cell().Element(CellStyle).AlignCenter().AlignMiddle().Text("");
                    table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                }
            }

            // step 3
            if (ArticleSearchResponse.Count > 0)
            {
                foreach (var item in ArticleSearchResponse)
                {
                    table.Cell().Element(CellStyle).AlignCenter().Text((ArticleSearchResponse.IndexOf(item) + 1).ToString());
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Sku);
                    table.Cell().Element(CellStyle).PaddingLeft(4).Row(row =>
                    {
                        row.RelativeItem(4).Text($"{item.Display}  ");
                        row.RelativeItem().AlignMiddle().Text(text =>
                        {
                            if (item.Delivered == true)
                            {
                                text.Span("ENTREGADO").Style(delivered);
                            }
                            if (item.Delivered == false)
                            {
                                text.Span("PENDIENTE").Style(pending);
                            }
                        });
                    });

                    table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text($"{item.Cost}");
                    table.Cell().Element(CellStyle).AlignCenter().Text((item.Quantity).ToString());
                    table.Cell().Element(CellStyle).PaddingRight(4).AlignRight().Text($"{item.Total}");
                }
            }
            

            table.Footer(footer =>
            {
                footer.Cell().RowSpan(3).ColumnSpan(4).Element(ComposePayments);
                footer.Cell().Row(1).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text("Subtotal: ").SemiBold();
                footer.Cell().Row(2).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text("IVA 12%: ").SemiBold();
                footer.Cell().Row(3).Column(5).Element(CellStyle).PaddingRight(4).AlignRight().Text("Total: ").SemiBold();
                if (ArticleSearchResponse.Count == 0)
                {
                    footer.Cell().Row(1).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                    footer.Cell().Row(2).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                    footer.Cell().Row(3).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                }
                if (ArticleSearchResponse.Count > 0)
                {
                    footer.Cell().Row(1).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text($"${_order.SubTotal}");
                    footer.Cell().Row(2).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text("");
                    footer.Cell().Row(3).Column(6).Element(CellStyle).PaddingRight(4).AlignRight().Text($"${_order.Total}").SemiBold();
                }

                //static IContainer Block(IContainer container)
                //{
                //    return container
                //        .Border(1)
                //        .Background(Colors.Grey.Lighten3)
                //        .ShowOnce()
                //        .MinWidth(50)
                //        .MinHeight(50)
                //        .AlignCenter()
                //        .AlignMiddle();
                //}
            });

            static IContainer CellStyle(IContainer container)
            {
                return container.MinHeight(25).Border(1).BorderColor(Colors.Black).AlignMiddle();
            }


        });        
    }

    void ComposePayments(IContainer container)
    {
        new PaymentComponent(_paymentMethodByIdResponse).Compose(container);
    }
}
