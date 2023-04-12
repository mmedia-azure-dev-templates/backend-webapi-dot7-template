using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
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


    public CustomerUpdateHandler(IMediator mediator,IContext context, IMapper mapper, ILogger<CustomerUpdateHandler> logger, ICustomerUpdateResponse customerUpdateResponse)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _customerUpdateResponse = (CustomerUpdateResponse)customerUpdateResponse;
    }
    public async Task<CustomerUpdateResponse> Handle(CustomerUpdateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var customer = _mapper.Map<Customer>(request);
            customer.Id = new CustomerId((Guid)request.CustomerId);
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);

            request.AddresUpdateRequest.PersonId = new PersonId((Guid)customer.Id);
            var addres = await _mediator.Send(request.AddresUpdateRequest);
            await _context.SaveChangesAsync(cancellationToken);
            _customerUpdateResponse = _mapper.Map(customer, _customerUpdateResponse);
            _customerUpdateResponse.CustomerId = customer.Id;
            _customerUpdateResponse.AddresUpdateResponse = addres;
            scope.Complete();
        }
            
        return _customerUpdateResponse;
    }
}
