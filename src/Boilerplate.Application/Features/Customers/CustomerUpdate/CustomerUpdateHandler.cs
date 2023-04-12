using Amazon.Runtime.Documents;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Address.AddresUpdate;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using org.apache.zookeeper.data;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Customers.CustomerUpdate;

public class CustomerUpdateHandler : IRequestHandler<CustomerUpdateRequest, CustomerUpdateResponse>
{
    private readonly IMediator _mediator;
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerUpdateHandler> _logger;
    private CustomerUpdateResponse _customerUpdateResponse;
    private AddresUpdateResponse _addresUpdateResponse;


    public CustomerUpdateHandler(
        IMediator mediator,
        IContext context, 
        IMapper mapper, 
        ILogger<CustomerUpdateHandler> logger, 
        ICustomerUpdateResponse customerUpdateResponse,
        IAddresUpdateResponse addresUpdateResponse)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _customerUpdateResponse = (CustomerUpdateResponse)customerUpdateResponse;
        _addresUpdateResponse = (AddresUpdateResponse)addresUpdateResponse;
    }
    public async Task<CustomerUpdateResponse> Handle(CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var customer = await _context.Customers.Where(x=> x.Id == request.CustomerId).FirstOrDefaultAsync(cancellationToken);
            if(customer != null)
            {
                //Insert Format Customer 
                customer.BirthDate = request.BirthDate;
                customer.GenderType = request.GenderType;
                customer.CivilStatusType = request.CivilStatusType;
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.Email = request.Email;
                customer.Mobile = request.Mobile;
                customer.Phone = request.Phone;
                customer.Notes = request.Notes;
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync(cancellationToken);
                _customerUpdateResponse = _mapper.Map(customer, _customerUpdateResponse);
                _customerUpdateResponse.CustomerId = customer.Id;

                //Insert Format Address
                request.AddresUpdateRequest.PersonId = new PersonId((Guid)customer.Id);
                _customerUpdateResponse.AddresUpdateResponse = await _mediator.Send(request.AddresUpdateRequest);
                await _context.SaveChangesAsync(cancellationToken);
            }

            scope.Complete();
        }
            
        return _customerUpdateResponse;
    }
}
