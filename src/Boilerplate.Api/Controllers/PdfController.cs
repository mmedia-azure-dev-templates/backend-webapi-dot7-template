using Boilerplate.Application.Features.Pdfs;
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
    [Route("CheckGenerationPdf")]
    [AllowAnonymous]
    public IActionResult GetPdf()
    {
        var model = InvoiceDocumentDataSource.GetInvoiceDetails();
        var document = new InvoiceDocument(model);
        return File(document.GeneratePdf(), "application/pdf", "myReport.pdf");
    }
}
