using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Pdfs;
using Boilerplate.Domain.Implementations;
using Microsoft.AspNetCore.Http;
using QuestPDF.Fluent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Boilerplate.Application.Services;
public class PdfService: IPdfService
{
    private readonly IAwsS3Service _awsS3Service;
    public PdfService(IAwsS3Service awsS3Service)
    {
        _awsS3Service = awsS3Service;
    }

    public async Task<string> GenerateOrderPdf(Order order, List<OrderItem> orderItems, Customer customer)
    {
        var orderDocumentDataSource = new OrderDocumentDataSource(order,orderItems,customer);
        var document = new OrderDocument(orderDocumentDataSource);
        var stream = document.GeneratePdf();
        //var orderDocument = new FormFile(document.GeneratePdf(), "application/pdf", "myReport.pdf");

        //AmazonObject objectImageProfile = await _awsS3Service.UploadFileAsync(document.GeneratePdf(), "public", "fotoperfil.jpg");
        //if (objectImageProfile.ObjectUrl == null)
        //{
        //    _userResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("UserResponseTitleError").Value;
        //    return _userResponse;
        //}
        return "pdf";
    }

}
