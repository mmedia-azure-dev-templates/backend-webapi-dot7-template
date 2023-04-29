using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

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
            var workbook = new XLWorkbook(excelFile.OpenReadStream());

            //var SheetNames = new List<string>() { "15-16", "16-17", "17-18", "18-19", "19-20" };
            //foreach (var sheetname in SheetNames)
            //{
            //    var worksheet = workbook.Worksheets.Add(sheetname);
            //    worksheet.Cell("A1").Value = sheetname;
            //}

            workbook.SaveAs(stream);
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