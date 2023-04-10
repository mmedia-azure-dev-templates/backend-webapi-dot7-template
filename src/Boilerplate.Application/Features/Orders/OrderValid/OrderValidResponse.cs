using Boilerplate.Application.Features.Orders.OrderById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderValid;
public class OrderValidResponse
{
    public bool IsValid { get; set; } = false;
    public OrderByIdResponse OrderByIdResponse { get; set; }
}
