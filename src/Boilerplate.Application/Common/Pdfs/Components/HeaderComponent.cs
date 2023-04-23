using Boilerplate.Application.Features.Orders.OrderById;
using Org.BouncyCastle.Utilities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections;
using System.IO;
using System.Net;

namespace Boilerplate.Application.Common.Pdfs.Components;
public class HeaderComponent : IComponent
{
    public WebClient _client = new WebClient();
    public OrderByIdResponse _orderByIdResponse { get; set; }
    public byte[] _logo { get; set; }
    public Stream? _stream { get; set; }
    public HeaderComponent(OrderByIdResponse orderByIdResponse)
    {
        _orderByIdResponse = orderByIdResponse;
        _logo = _client.DownloadData("https://mad-storage.s3.amazonaws.com/public/logomarket.png");
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
                        text.Span($"{_orderByIdResponse.Order.OrderNumber}").Bold().Style(titleStyle);

                    });
                });

                row.RelativeItem(1).Image(_logo, ImageScaling.FitArea);
            });

            column.Item().Text(text =>
            {
                text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                text.Span("Fecha: ").SemiBold();
                text.Span($"{_orderByIdResponse.Order.DateCreated:F}").Light();
                text.EmptyLine();

                text.Span($"No. Cédula: ").SemiBold();
                if (_orderByIdResponse.Customer?.Ndocument != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.Ndocument}").Light();
                }
                if (_orderByIdResponse.Customer?.Ndocument == null)
                {
                    text.Span("_________________").Light();
                }
                text.EmptyLine();

                text.Span("Nombres y Apellidos: ").SemiBold();
                if (_orderByIdResponse.Customer?.FirstName != null && _orderByIdResponse.Customer?.LastName != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.FirstName} {_orderByIdResponse.Customer?.LastName}").Light();
                }
                if (_orderByIdResponse.Customer?.FirstName == null || _orderByIdResponse.Customer?.LastName == null)
                {
                    text.Span("___________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Dirección domiciliar: ").SemiBold();
                if (
                    _orderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet != null &&
                    _orderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet != null &&
                    _orderByIdResponse.Customer?.AddressByIdResponse?.Numeration != null
                    )
                {
                    text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet} {_orderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet} {_orderByIdResponse.Customer?.AddressByIdResponse?.Numeration}").Light();
                }
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet == null ||
                    _orderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet == null ||
                    _orderByIdResponse.Customer?.AddressByIdResponse?.Numeration == null)
                {
                    text.Span("__________________________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Referencia del domicilio: ").SemiBold();
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.Reference != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.Reference}").Light();
                }
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.Reference == null)
                {
                    text.Span("_____________________________________________________").Light();
                }
                text.EmptyLine();

                text.Span("Teléfonos: ").SemiBold();
                if (_orderByIdResponse.Customer?.Mobile != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.Mobile}").Light();
                }
                if (_orderByIdResponse.Customer?.Mobile == null)
                {
                    text.Span("___________________________").Light();
                }
                text.EmptyLine();

                text.Span("Correo electrónico: ").SemiBold();
                if (_orderByIdResponse.Customer?.Email != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.Email}").Light();
                }
                if (_orderByIdResponse.Customer?.Email == null)
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
                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay != null)
                            {
                                text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay} ").Light();
                            }
                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay == null)
                            {
                                text.Span("______________________").Light();
                            }
                        });

                    row.ConstantItem(160)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                            text.Span("Canton: ").SemiBold();
                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay != null)
                            {
                                text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay} ").Light();
                            }

                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay == null)
                            {
                                text.Span("________________________").Light();
                            }
                        });

                    row.RelativeItem(2)
                        .Text(text =>
                        {
                            text.DefaultTextStyle(TextStyle.Default.LineHeight(1.5f));
                            text.Span("Parroquia: ").SemiBold();
                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay != null)
                            {
                                text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay} ").Light();
                            }
                            if (_orderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay == null)
                            {
                                text.Span("______________________").Light();
                            }
                        });
                });
            });
        });
    }
}