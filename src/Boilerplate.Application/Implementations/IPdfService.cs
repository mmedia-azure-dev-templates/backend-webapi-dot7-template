using Boilerplate.Application.Common.Pdfs;
using Boilerplate.Domain.Entities.Common;
using System.Threading.Tasks;

namespace Boilerplate.Application.Implementations;
public interface IPdfService
{
    public Task<AmazonObject> CreateOrderPdf(OrderDocument orderDocument);
}
