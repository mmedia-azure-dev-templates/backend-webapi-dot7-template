using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Boilerplate.Application.Features.Orders.OrderByNumber;
public class OrderByNumberHandler : IRequestHandler<OrderByNumberRequest, OrderByNumberResponse>
{
    public readonly IContext _context;
    public OrderByNumberHandler(IContext context)
    {
        _context = context;
    }

    public async Task<OrderByNumberResponse> Handle(OrderByNumberRequest request, CancellationToken cancellationToken)
    {
        var orderByIdResponse = new OrderByNumberResponse();
        var result = await (from order in _context.Orders.AsNoTracking()
                            join orderItems in _context.OrderItems.AsNoTracking() on order.Id equals orderItems.OrderId
                            join customer in _context.Customers.AsNoTracking() on order.CustomerId equals customer.Id
                            where order.OrderNumber == request.OrderNumber
                            select new { order, orderItems, customer }).ToListAsync(cancellationToken);

        orderByIdResponse.Order = result.Select(x => x.order).FirstOrDefault();
        orderByIdResponse.OrderItems = result.Select(x => x.orderItems).ToList();
        orderByIdResponse.Customer = result.Select(x => x.customer).FirstOrDefault();
        return orderByIdResponse;
    }
}