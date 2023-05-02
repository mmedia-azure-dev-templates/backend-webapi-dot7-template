using Boilerplate.Application.Features.ArticlesItems.ArticleItemCreateUpdateBySku;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Entities.Excels;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExcelController : ControllerBase
{
    private readonly IMediator _mediator;
    public ExcelController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("excel")]
    public async Task<IActionResult> ExcelFile(IFormFile excelFile)
    {
        //https://stackoverflow.com/questions/22296136/download-file-with-closedxml
        using (MemoryStream stream = new MemoryStream())
        {
            foreach (var fontFamily in SixLabors.Fonts.SystemFonts.Collection.Families)
                Console.WriteLine(fontFamily.Name);
            var workBook = new XLWorkbook(excelFile.OpenReadStream());
            var workSheet = workBook.Worksheet(1);
            var columns = workSheet.FirstRowUsed();
            HeaderArticlesPrices header = new HeaderArticlesPrices();

            if (columns.Cell(1).Value.IsBlank || columns.Cell(2).Value.IsBlank)
            {
                return BadRequest("El archivo no tiene el formato correcto");
            }

            header.Sku = columns.Cell(1).Value.ToString();
            header.Display = columns.Cell(2).Value.ToString();

            if (columns.Cell(3).Value.IsBlank == false)
            {
                header.CashPayment = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(3).Value.ToString());
            }

            if (columns.Cell(4).Value.IsBlank == false)
            {
                header.CreditCard = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(4).Value.ToString());
            }

            if (columns.Cell(5).Value.IsBlank == false)
            {
                header.DirectCredit = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(5).Value.ToString());
            }

            if (columns.Cell(6).Value.IsBlank == false)
            {
                header.Fcme = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(6).Value.ToString());
            }

            var rows = workSheet.RowsUsed().Skip(1);
            foreach (var row in rows)
            {
                bool isEmpty = false;

                foreach (IXLCell cell in row.Cells())
                {
                    if (cell.IsEmpty())
                    {
                        isEmpty = true;
                    }
                }

                ArticleItemUpdateBySkuRequest articleItemUpdateBySkuRequest = new ArticleItemUpdateBySkuRequest
                {
                    Sku = row.Cell(1).Value.ToString(),
                    Display = row.Cell(2).Value.ToString()
                };

                if (header.CashPayment != null && isEmpty == false)
                {
                    articleItemUpdateBySkuRequest.PaymentMethodsType = (PaymentMethodsType)header.CashPayment;
                    articleItemUpdateBySkuRequest.Price = Convert.ToDecimal(row.Cell(3).Value.ToString());
                }

                if (header.CreditCard != null && isEmpty == false)
                {
                    articleItemUpdateBySkuRequest.PaymentMethodsType = (PaymentMethodsType)header.CreditCard;
                    articleItemUpdateBySkuRequest.Price = Convert.ToDecimal(row.Cell(4).Value.ToString());
                }

                if (header.DirectCredit != null && isEmpty == false)
                {
                    articleItemUpdateBySkuRequest.PaymentMethodsType = (PaymentMethodsType)header.DirectCredit;
                    articleItemUpdateBySkuRequest.Price = Convert.ToDecimal(row.Cell(5).Value.ToString());
                }

                if (header.Fcme != null && isEmpty == false)
                {
                    articleItemUpdateBySkuRequest.PaymentMethodsType = (PaymentMethodsType)header.Fcme;
                    articleItemUpdateBySkuRequest.Price = Convert.ToDecimal(row.Cell(6).Value.ToString());
                }

                await _mediator.Send(articleItemUpdateBySkuRequest);
            }

            stream.Seek(0, SeekOrigin.Begin);

            return File(
                fileContents: stream.ToArray(),
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                // By setting a file download name the framework will
                // automatically add the attachment Content-Disposition header
                fileDownloadName: "ERSheet.xlsx"
            );
        }
    }

}