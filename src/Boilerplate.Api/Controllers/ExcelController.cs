﻿using Boilerplate.Application.Features.Articles.ArticleItemCreateUpdateBySku;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Entities.Excels;
using ClosedXML.Excel;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;
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
    [Route("uploadexcelarticles")]
    public async Task<IActionResult> UploadExcelArticles(IFormFile excelFile)
    {
        //https://stackoverflow.com/questions/22296136/download-file-with-closedxml
        using (MemoryStream memoryStream = new MemoryStream())
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

            var rows = workSheet.Rows().Skip(1);
            TextWriter articleLog = new StreamWriter(memoryStream);
            //{
            //    tw.Write("blabla");
            //    tw.Flush();
            //    memoryStream.Position = 0;
            //    memoryStream.Close();
            //}
            foreach (var row in rows)
            {

                bool isEmpty = false;
                //row.RowNumber();
                foreach (IXLCell cell in workSheet.Range($"{row.FirstCell()}:{row.LastCellUsed()}").Cells())
                {
                    if (cell.IsEmpty())
                    {
                        isEmpty = true;
                        break;
                    }
                }

                if (header.CashPayment != null && isEmpty == false)
                {
                    ArticleItemUpdateBySkuRequest articleItemUpdateBySkuRequest = new ArticleItemUpdateBySkuRequest
                    {
                        Sku = row.Cell(1).Value.ToString(),
                        Display = row.Cell(2).Value.ToString(),
                        PaymentMethodsType = (PaymentMethodsType)header.CashPayment,
                        Price = Convert.ToDecimal(row.Cell(3).Value.ToString())
                    };
                    articleLog.Write(row.Cell(1).Value.ToString());
                    articleLog.WriteLine();
                    await _mediator.Send(articleItemUpdateBySkuRequest);
                }

                if (header.CreditCard != null && isEmpty == false)
                {
                    ArticleItemUpdateBySkuRequest articleItemUpdateBySkuRequest = new ArticleItemUpdateBySkuRequest
                    {
                        Sku = row.Cell(1).Value.ToString(),
                        Display = row.Cell(2).Value.ToString(),
                        PaymentMethodsType = (PaymentMethodsType)header.CreditCard,
                        Price = Convert.ToDecimal(row.Cell(4).Value.ToString())
                    };
                    articleLog.Write(row.Cell(1).Value.ToString());
                    articleLog.WriteLine();
                    await _mediator.Send(articleItemUpdateBySkuRequest);
                }

                if (header.DirectCredit != null && isEmpty == false)
                {
                    ArticleItemUpdateBySkuRequest articleItemUpdateBySkuRequest = new ArticleItemUpdateBySkuRequest
                    {
                        Sku = row.Cell(1).Value.ToString(),
                        Display = row.Cell(2).Value.ToString(),
                        PaymentMethodsType = (PaymentMethodsType)header.DirectCredit,
                        Price = Convert.ToDecimal(row.Cell(5).Value.ToString())
                    };
                    articleLog.Write(row.Cell(1).Value.ToString());
                    articleLog.WriteLine();
                    await _mediator.Send(articleItemUpdateBySkuRequest);
                }

                if (header.Fcme != null && isEmpty == false)
                {
                    ArticleItemUpdateBySkuRequest articleItemUpdateBySkuRequest = new ArticleItemUpdateBySkuRequest
                    {
                        Sku = row.Cell(1).Value.ToString(),
                        Display = row.Cell(2).Value.ToString(),
                        PaymentMethodsType = (PaymentMethodsType)header.Fcme,
                        Price = Convert.ToDecimal(row.Cell(6).Value.ToString())
                    };
                    articleLog.Write(row.Cell(1).Value.ToString());
                    articleLog.WriteLine();
                    await _mediator.Send(articleItemUpdateBySkuRequest);
                }

                
            }

            //stream.Seek(0, SeekOrigin.Begin);
            articleLog.Flush();
            memoryStream.Close();
            return File(memoryStream.ToArray(),"application/force-download", "articleLog.txt");
        }
    }

}