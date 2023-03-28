using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.OrderItems.OrderItemCreate;
public class OrderItemCreateHandler : IRequestHandler<OrderItemCreateRequest, OrderItemCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private ArticleCreateResponse _articleCreateResponse;

    public OrderItemCreateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<OrderItemCreateResponse> Handle(OrderItemCreateRequest request, CancellationToken cancellationToken)
    {
        var orderItem = new OrderItem {
            ArticleId = request.ArticleId,
            Quantity = request.Quantity,
            Price = request.Price,
            Total = request.Total
        };

        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync(cancellationToken);
        return new OrderItemCreateResponse { OrderItemId = orderItem.Id };
    }
}