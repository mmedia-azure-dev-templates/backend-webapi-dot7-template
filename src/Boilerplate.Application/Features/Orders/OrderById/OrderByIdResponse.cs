using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdResponse
{
    public Order Order { get; set; }
    public List<OrderItem> OrderItems { get; set; }
    public Customer Customer { get; set; }
}
