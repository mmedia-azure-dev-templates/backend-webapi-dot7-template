using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Pdfs;
using Boilerplate.Domain.Implementations;
using QuestPDF.Fluent;
using System.Collections.Generic;
using System.Threading.Tasks;
using ISession = Boilerplate.Domain.Implementations.ISession;

namespace Boilerplate.Application.Services;
public class PdfService: IPdfService
{
    private readonly IAwsS3Service _awsS3Service;
    private readonly ISession _session;
    public PdfService(IAwsS3Service awsS3Service, ISession session)
    {
        _awsS3Service = awsS3Service;
        _session = session;
    }

    public async Task<AmazonObject> GenerateOrderPdf(Order order, List<OrderItem> orderItems, Customer customer)
    {
        var relativePath = _session.TenantName + "/orders/" + _session.Now.Year + "/"+ _session.Now.ToString("MM") + "/" + _session.Now.ToString("dd") + "/" + order.OrderNumber.ToString();
        var orderDocumentDataSource = new OrderDocumentDataSource(order,orderItems,customer);
        var document = new OrderDocument(orderDocumentDataSource);
        AmazonObject amazonObject = await _awsS3Service.UploadFileAmazonAsync(document.GeneratePdf(), relativePath, "order.pdf");
        return amazonObject;
    }
}
