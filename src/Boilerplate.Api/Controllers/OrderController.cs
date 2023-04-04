using Boilerplate.Application.Features.OrderItems.OrderItemById;
using Boilerplate.Application.Features.OrderItems.OrderItemCreate;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Orders.OrderByNumber;
using Boilerplate.Application.Features.Orders.OrderCreate;
using Boilerplate.Application.Features.Orders.OrderUpdate;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("getorderbyid")]
    public async Task<OrderByIdResponse> GetOrderById([FromQuery]OrderByIdRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    [Route("getorderbynumber")]
    public async Task<OrderByNumberResponse> GetOrderByNumber([FromQuery] OrderByNumberRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("create")]
    public async Task<OrderCreateResponse> Create(OrderCreateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [Route("update")]
    public async Task<OrderUpdateResponse> Update(OrderUpdateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    [Route("getorderitem")]
    public async Task<OrderItemByIdResponse> GetOrderItem(OrderItemByIdRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("itemcreate")]
    public async Task<OrderItemCreateResponse> OrderItemCreate(OrderItemCreateRequest request)
    {
        return await _mediator.Send(request);
    }
}
