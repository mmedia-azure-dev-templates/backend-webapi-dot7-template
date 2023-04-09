using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Orders.OrderCreate;
using Boilerplate.Application.Features.Orders.OrderSearch;
using Boilerplate.Application.Features.Users.GetUserById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdHandler : IRequestHandler<OrderByIdRequest, OrderByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;
    private OrderByIdResponse _orderByIdResponse;
    public OrderByIdHandler(IContext context, IMapper mapper, IOrderService orderService, IOrderByIdResponse orderByIdResponse)
    {
        _context = context;
        _mapper = mapper;
        _orderService = orderService;
        _orderByIdResponse = (OrderByIdResponse)orderByIdResponse;
    }

    public async Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var hola = await _orderService.GetLocalOrderById(request.OrderId).ToListAsync(cancellationToken);
        

        _orderByIdResponse = _mapper.Map<OrderByIdResponse>(hola);

        return _orderByIdResponse;
    }
}