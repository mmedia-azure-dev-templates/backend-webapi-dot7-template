using Boilerplate.Application.Features.Orders.OrderById;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Net;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class HeaderComponent : IComponent
{
    readonly WebClient _client = new WebClient();
    public OrderByIdResponse OrderByIdResponse { get; set; }
    public byte[] Logo { get; set; } = new byte[0];
    public HeaderComponent(OrderByIdResponse orderByIdResponse)
    {
        OrderByIdResponse = orderByIdResponse;
        Logo = _client.DownloadData("https://mad-storage.s3.amazonaws.com/public/logomarket.png");
    }

    public void Compose(IContainer container)
    {
        var titleStyle = TextStyle.Default.FontSize(15).SemiBold().FontColor(Colors.Red.Medium);
        container.Column(column =>
        {
            column.Item().Row(row =>
            {
                row.ConstantItem(300).AlignMiddle().Column(column =>
                {
                    column.Item().Text(text =>
                    {
                        text.Span($"ORDEN DE COMPRA No. ").Bold().FontSize(16);
                        text.Span($"{OrderByIdResponse.Order.OrderNumber}").Bold().Style(titleStyle);

                    });
                });

                row.RelativeItem(1).Image(Logo!, ImageScaling.FitArea);
            });

            column.Item().Text(text =>
            {
                text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                text.Span("Fecha: ").SemiBold();
                text.Span($"{OrderByIdResponse.Order.DateCreated:F}").Light();
                text.EmptyLine();

                text.Span($"No. Cédula: ").SemiBold();
                if (OrderByIdResponse.Customer?.Ndocument != null)
                {
                    text.Span($"{OrderByIdResponse.Customer?.Ndocument}").Light();
                }
                if (OrderByIdResponse.Customer?.Ndocument == null)
                {
                    text.Span("_________________").Light();
                }
                text.EmptyLine();

                text.Span("Nombres y Apellidos: ").SemiBold();
                if (OrderByIdResponse.Customer?.FirstName != null && OrderByIdResponse.Customer?.LastName != null)
                {
                    text.Span($"{OrderByIdResponse.Customer?.FirstName} {OrderByIdResponse.Customer?.LastName}").Light();
                }
                if (OrderByIdResponse.Customer?.FirstName == null || OrderByIdResponse.Customer?.LastName == null)
                {
                    text.Span("___________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Dirección domiciliar: ").SemiBold();
                if (
                    OrderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet != null &&
                    OrderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet != null &&
                    OrderByIdResponse.Customer?.AddressByIdResponse?.Numeration != null
                    )
                {
                    text.Span($"{OrderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet} {OrderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet} {OrderByIdResponse.Customer?.AddressByIdResponse?.Numeration}").Light();
                }
                if (OrderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet == null ||
                    OrderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet == null ||
                    OrderByIdResponse.Customer?.AddressByIdResponse?.Numeration == null)
                {
                    text.Span("__________________________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Referencia del domicilio: ").SemiBold();
                if (OrderByIdResponse.Customer?.AddressByIdResponse?.Reference != null)
                {
                    text.Span($"{OrderByIdResponse.Customer?.AddressByIdResponse?.Reference}").Light();
                }
                if (OrderByIdResponse.Customer?.AddressByIdResponse?.Reference == null)
                {
                    text.Span("_____________________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Teléfonos: ").SemiBold();
                if (OrderByIdResponse.Customer?.Mobile != null)
                {
                    text.Span($"{OrderByIdResponse.Customer?.Mobile}").Light();
                }
                if (OrderByIdResponse.Customer?.Mobile == null)
                {
                    text.Span("___________________________").Light();
                }
                text.EmptyLine();

                text.Span("Correo electrónico: ").SemiBold();
                if (OrderByIdResponse.Customer?.Email != null)
                {
                    text.Span($"{OrderByIdResponse.Customer?.Email}").Light();
                }
                if (OrderByIdResponse.Customer?.Email == null)
                {
                    text.Span("__________________________________").Light();
                }
                
                column.Item().Row(row =>
                {
                    row.Spacing(5);
                    row.ConstantItem(160)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                            text.Span("Provincia: ").SemiBold();
                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay != null)
                            {
                                text.Span($"{OrderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay} ").Light();
                            }
                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay == null)
                            {
                                text.Span("______________________").Light();
                            }
                        });

                    row.ConstantItem(160)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                            text.Span("Canton: ").SemiBold();
                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay != null)
                            {
                                text.Span($"{OrderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay} ").Light();
                            }

                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay == null)
                            {
                                text.Span("________________________").Light();
                            }
                        });

                    row.RelativeItem(2)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                            text.Span("Parroquia: ").SemiBold();
                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay != null)
                            {
                                text.Span($"{OrderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay} ").Light();
                            }
                            if (OrderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay == null)
                            {
                                text.Span("______________________").Light();
                            }
                        });
                });
            });
        });
    }
}