using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
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

    public async Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var orderByIdResponse = new OrderByIdResponse();
        var result = await (from order in _context.Orders.AsNoTracking()
                            join orderItems in _context.OrderItems.AsNoTracking() on order.Id equals orderItems.OrderId
                            join customer in _context.Customers.AsNoTracking() on order.CustomerId equals customer.Id
                            where order.Id == request.OrderId
                            select new { order, orderItems, customer }).ToListAsync(cancellationToken);

        orderByIdResponse.Order = result.Select(x => x.order).FirstOrDefault();
        orderByIdResponse.OrderItems = result.Select(x => x.orderItems).ToList();
        orderByIdResponse.Customer = result.Select(x => x.customer).FirstOrDefault();
        return orderByIdResponse;
    }
}