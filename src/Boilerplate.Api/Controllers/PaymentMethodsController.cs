using Boilerplate.Application.Common;
using Boilerplate.Application.Features.OrderItems.OrderItemById;
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
    public async Task <List<PaymentMethod>> PaymentMethods()
    {
        return await (from t in _context.PaymentMethods
                     select t).ToListAsync();
    }

    [HttpPost]
    [Route("create")]
    public async Task <PaymentMethodCreateResponse> Create(PaymentMethodCreateRequest request)
    {
        return await _mediator.Send(request);
        
    }
    
}
