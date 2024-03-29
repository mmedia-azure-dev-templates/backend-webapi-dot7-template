﻿using AuthPermissions.AspNetCore;
using AuthPermissions.BaseCode.PermissionsCode;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.PermissionsCode;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IContext _context;

    public InvoiceController(IContext context)
    {
        _context = context;
    }

    [HttpGet]
    [Route("invoices")]
    [HasPermission(DefaultPermissions.InvoiceRead)]
    public async Task<List<InvoiceSummaryDto>> Invoices()
    {
        var listInvoices = await InvoiceSummaryDto.SelectInvoices(_context.Invoices)
                .OrderByDescending(x => x.DateCreated)
                .ToListAsync();
        return listInvoices;
    }

    [HttpPost]
    [Route("createinvoice")]
    [HasPermission(DefaultPermissions.InvoiceCreate)]
    public IActionResult CreateInvoice(Invoice invoice)
    {
        return Ok("Hola");
        //var builder = new ExampleInvoiceBuilder(null);
        //var newInvoice = builder.CreateRandomInvoice(AddTenantNameClaim.GetTenantNameFromUser(User), invoice.InvoiceName);
        //_context.Add(newInvoice);
        //await _context.SaveChangesAsync();

        //return RedirectToAction("Index", new { message = $"Added the invoice '{newInvoice.InvoiceName}'." });
    }

}
