using Boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Implementations;
public interface IPdfService
{
    public Task<string> GenerateOrderPdf(Order order, List<OrderItem> orderItems, Customer customer);
}
