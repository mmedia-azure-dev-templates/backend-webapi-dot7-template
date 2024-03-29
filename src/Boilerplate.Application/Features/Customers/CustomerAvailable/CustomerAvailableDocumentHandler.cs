﻿using AuthPermissions.AdminCode;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users.CreateUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Customers.CustomerAvailable;
public class CustomerAvailableDocumentHandler : IRequestHandler<CustomerAvailableDocumentRequest, CustomerAvailableDocumentResponse>
{
    private readonly IContext _context;
    public CustomerAvailableDocumentHandler(IContext context)
    {
        _context = context;
    }

    public async Task<CustomerAvailableDocumentResponse> Handle(CustomerAvailableDocumentRequest request, CancellationToken cancellationToken)
    {
        CustomerAvailableDocumentResponse customerAvailableDocumentResponse = new CustomerAvailableDocumentResponse();
        customerAvailableDocumentResponse.IsAvailable = true;

        var customerNdocument = await _context.Customers.Where(x => x.DocumentType == request.DocumentType && x.Ndocument == request.Ndocument).FirstOrDefaultAsync();
        if (customerNdocument != null)
        {
            customerAvailableDocumentResponse.IsAvailable = false;
            customerAvailableDocumentResponse.CustomerId = customerNdocument.Id;
        }

        return customerAvailableDocumentResponse;
    }
}
