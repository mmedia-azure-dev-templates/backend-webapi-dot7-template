using Boilerplate.Application.Common;
using Boilerplate.Application.Features.OrderItems.OrderItemById;
using Boilerplate.Application.Features.Orders;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class PaymentMethodsController : ControllerBase
{
    private readonly IContext _context;
    private readonly IMediator _mediator;
    public PaymentMethodsController(IContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("paymentmethods")]
    public async Task <List<PaymentMethodAllResponse>> PaymentMethods()
    {
        var result = await (from t in _context.PaymentMethods
                     select t).ToListAsync();
        List<PaymentMethodAllResponse> paymentMethodAllResponse = new List<PaymentMethodAllResponse>();
        foreach (var item in result)
        {
            paymentMethodAllResponse.Add(
                new PaymentMethodAllResponse
                {
                    Id = item.Id,
                    PaymentMethodsType = item.PaymentMethodsType,
                    Display = item.Display,
                    DataKey = item.DataKey,
                    Icon = item.Icon,
                    Active = item.Active,
                    DateCreated = item.DateCreated,
                    DateUpdated = item.DateUpdated
                }
            );
        }
        return paymentMethodAllResponse;
    }

    [HttpPost]
    [Route("create")]
    public async Task <PaymentMethodCreateResponse> Create(PaymentMethodCreateRequest request)
    {
        return await _mediator.Send(request);
        
    }
    
}
