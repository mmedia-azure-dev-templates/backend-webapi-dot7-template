using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfHandler : IRequestHandler<OrderPdfRequest, OrderPdfResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    public OrderPdfHandler(IContext context, IMapper mapper,IOrderService orderService)
    {
        _context = context;
        _mapper = mapper;
        _orderService = orderService;
    }

    public async Task<OrderPdfResponse> Handle(OrderPdfRequest request, CancellationToken cancellationToken)
    {
        var orderValid = await _orderService.CheckValidOrderById(request.OrderId, cancellationToken);
        throw new NotImplementedException();
    }
}