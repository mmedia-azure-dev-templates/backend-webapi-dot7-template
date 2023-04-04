using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IPdfService
{
    public Task<AmazonObject> GenerateOrderPdf(Order order, List<OrderItem> orderItems, Customer customer);
}
