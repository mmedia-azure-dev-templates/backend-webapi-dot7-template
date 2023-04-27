using Boilerplate.Application.Common;
using Boilerplate.Application.Features.PaymentMethods;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodCreate;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodPriorityUpdate;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodUpdate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                            orderby t.Priority
                     select t).ToListAsync();
        List<PaymentMethodAllResponse> paymentMethodAllResponse = new List<PaymentMethodAllResponse>();
        foreach (var item in result)
        {
            paymentMethodAllResponse.Add(
                new PaymentMethodAllResponse
                {
                    Id = item.Id,
                    DataKey = item.DataKey,
                    PaymentMethodsType = item.PaymentMethodsType,
                    Display = item.Display,
                    Priority = item.Priority,
                    Active = item.Active,
                    Icon = item.Icon,
                    DateCreated = item.DateCreated,
                    DateUpdated = item.DateUpdated
                }
            );
        }
        return paymentMethodAllResponse;
    }

    [HttpGet]
    [Route("getpaymentmethodbyid")]
    public async Task<PaymentMethodByIdResponse> GetPaymentMethodById([FromQuery] PaymentMethodByIdRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("create")]
    public async Task <PaymentMethodCreateResponse> Create(PaymentMethodCreateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPatch]
    [Route("edit")]
    public async Task<PaymentMethodUpdateResponse> Edit(PaymentMethodUpdateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPatch]
    [Route("priority")]
    public async Task<PaymentMethodPriorityUpdateResponse> Priority([FromBody]PaymentMethodPriorityUpdateRequest request)
    {
        return await _mediator.Send(request);
    }

}
