using Boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderByNumber;
public class OrderByNumberResponse
{
    public Order Order { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public Customer Customer { get; set; }
}