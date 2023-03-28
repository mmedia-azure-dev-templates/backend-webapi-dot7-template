using AuthPermissions.AdminCode;
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

namespace Boilerplate.Application.Features.Users.AvailableUserDocument;
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

        var customerNdocument = await _context.Customers.Where(x => x.Ndocument == request.Ndocument).FirstOrDefaultAsync();
        if (customerNdocument != null)
        {
            customerAvailableDocumentResponse.IsAvailable = false;
        }

        return customerAvailableDocumentResponse;
    }
}
