using Boilerplate.Domain.Entities.Pdfs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
namespace Boilerplate.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PdfController : Controller
{
    public PdfController()
    {

    }

    [HttpGet]
    [Route("GetPdf")]
    [AllowAnonymous]
    public IActionResult GetPdf()
    {
        var filePath = "invoice.pdf";

        var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        var document = new InvoiceDocument(model);
        document.GeneratePdf(filePath);
        return Ok(filePath);

    }
}
