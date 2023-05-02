using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Entities.Excels;
using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ExcelController : ControllerBase
{
    public ExcelController()
    {
        
    }

    [HttpPost]
    [Route("excel")]
    public IActionResult ExcelFile(IFormFile excelFile)
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

            if(columns.Cell(1).Value.IsBlank || columns.Cell(2).Value.IsBlank)
            {
                return BadRequest("El archivo no tiene el formato correcto");
            }

            header.Sku = columns.Cell(1).Value.ToString();
            header.Display = columns.Cell(2).Value.ToString();

            if (columns.Cell(3).Value.IsBlank == false)
            {
                header.DirectCredit = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(3).Value.ToString());
            }

            if (columns.Cell(4).Value.IsBlank == false)
            {
                header.Fcme = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), columns.Cell(4).Value.ToString());
            }

            var rows = workSheet.RowsUsed().Skip(1);
            var articlesItems = new List<ArticleItem>();
            foreach (var row in rows)
            {
                var articleItem = new ArticleItem();
                if(header.DirectCredit != null)
                {
                    
                    //articleItem.DateUpdated = row.Cell(1).Value.ToString();
                    //articleItem.PaymentMethodId = (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), row.Cell(3).Value.ToString());
                    //return BadRequest("El archivo no tiene el formato correcto");

                }
                var hola = row.Cell(1).Value;
                var rowNumber = row.RowNumber();
            }

            //workbook.SaveAs(stream);
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