using Boilerplate.Application.Features.Orders.OrderValid;
using Boilerplate.Application.Features.Pdfs;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using QuestPDF.Fluent;
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

    public async Task<AmazonObject> CreateOrderPdf(OrderDocument orderDocument)
    {
        var relativePath = _session.TenantName + "/orders/" + _session.Now.Year + "/"+ _session.Now.ToString("MM") + "/" + _session.Now.ToString("dd") + "/" + orderDocument._orderValidResponse.OrderByIdResponse.Order.OrderNumber.ToString();
        AmazonObject amazonObject = await _awsS3Service.UploadFileAmazonAsync(orderDocument.GeneratePdf(), relativePath, "order.pdf");
        return amazonObject;
    }
}
