using Boilerplate.Application.Common;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdHandler : IRequestHandler<OrderByIdRequest, OrderByIdResponse>
{
    public readonly IContext _context;
    public OrderByIdHandler(IContext context)
    {
        _context = context;
    }

    public Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        OrderByIdResponse orderByIdResponse = new OrderByIdResponse();

        throw new NotImplementedException();
    }
}
