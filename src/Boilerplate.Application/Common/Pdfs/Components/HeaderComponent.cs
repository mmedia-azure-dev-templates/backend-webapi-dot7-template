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
    public WebClient client = new WebClient();
    public OrderByIdResponse _orderByIdResponse { get; set; }
    public byte[] _logo { get; set; }
    public Stream? _stream { get; set; }
    public HeaderComponent(OrderByIdResponse orderByIdResponse)
    {
        _orderByIdResponse = orderByIdResponse;
        _logo = client.DownloadData("https://mad-storage.s3.amazonaws.com/public/logomarket.png");
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
                text.Span("Nombres y Apellidos: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.FirstName} {_orderByIdResponse.Customer?.LastName}").Light();
                text.EmptyLine();
                text.Span($"No. Cédula: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.Ndocument}").Light();
                text.EmptyLine();
                text.Span("Dirección domiciliar: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.PrimaryStreet} {_orderByIdResponse.Customer?.AddressByIdResponse?.SecondaryStreet} {_orderByIdResponse.Customer?.AddressByIdResponse?.Numeration}").Light();
                text.EmptyLine();
                text.Span("Referencia del domicilio: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.Reference}").Light();
                text.EmptyLine();
                text.Span("Teléfonos: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.Mobile}").Light();
                text.EmptyLine();
                text.Span("Correo electrónico: ").SemiBold();
                text.Span($"{_orderByIdResponse.Customer?.Email}").Light();
                text.EmptyLine();
                text.Span("Provincia: ").SemiBold();
                if(_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay} ").Light();
                }
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.ProvinciaDisplay == null)
                {
                    
                }


                text.Span("Canton: ").SemiBold();
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay} ").Light();
                }
                text.Span("Parroquia: ").SemiBold();
                if (_orderByIdResponse.Customer?.AddressByIdResponse?.CantonDisplay != null)
                {
                    text.Span($"{_orderByIdResponse.Customer?.AddressByIdResponse?.ParroquiaDisplay} ").Light();
                }
            });

        });
    }
}