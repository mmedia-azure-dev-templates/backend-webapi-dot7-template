using Amazon.Runtime.Internal;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.OrderItems.OrderItemCreate;
public class OrderItemCreateRequest : IRequest<OrderItemCreateResponse>
{
    public ArticleId ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}
