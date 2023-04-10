using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Orders.OrderPdf;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderValid;
public class OrderValidHandler : IRequestHandler<OrderValidRequest, OrderValidResponse>
{
    IMediator _mediator;
    public OrderValidHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<OrderValidResponse> Handle(OrderValidRequest request, CancellationToken cancellationToken)
    {
        var result = new OrderValidResponse();
        var order = await _mediator.Send(new OrderByIdRequest { OrderId = request.OrderId }, cancellationToken);
        result.OrderByIdResponse = order;

        var products = order.ArticleSearchResponse.Count();

        if(products.Equals(0))
        {
            return result;
        }

        if(order.UserAssigned == null)
        {
            return result;
        }

        if(order.Customer == null)
        {
            return result;
        }

        result.IsValid = true;
        return result;
    }
}