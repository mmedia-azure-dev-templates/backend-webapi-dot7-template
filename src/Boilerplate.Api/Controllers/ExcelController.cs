using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
    public IActionResult ImgeProfile(IFormFile excelFile)
    {
        foreach (var fontFamily in SixLabors.Fonts.SystemFonts.Collection.Families)
            Console.WriteLine(fontFamily.Name);
        using var workbook = new XLWorkbook(excelFile.OpenReadStream());
        //var ws = workbook.Worksheet(1);
        workbook.SaveAs("HelloWorld.xlsx");
        return Ok("Fixed Excel");
    }

}