using Boilerplate.Application.Common;
using Boilerplate.Application.Features.OrderItems.OrderItemById;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
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
public class PaymentMethodsController : ControllerBase
{
    private readonly IContext _context;
    public PaymentMethodsController(IContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("paymentmethods")]
    public async Task <List<PaymentMethod>> PaymentMethods()
    {
        return await (from t in _context.PaymentMethods
                     select t).ToListAsync();
    }
}
