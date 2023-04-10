using Boilerplate.Application.Features.Orders.OrderValid;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Pdfs;
public interface IPdfService
{
    public Task<AmazonObject> CreateOrderPdf(OrderDocument orderDocument);
}
