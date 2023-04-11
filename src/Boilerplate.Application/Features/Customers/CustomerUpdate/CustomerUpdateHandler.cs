using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.Extensions.Logging;
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
            _context.Customers.Update(customer);

            //var address = new Addres
            //{
            //    PrimaryStreet = request.PrimaryStreet,
            //    SecondaryStreet = request.SecondaryStreet,
            //    Numeration = request.Numeration,
            //    Reference = request.Reference,
            //    Provincia = request.Provincia,
            //    Canton = request.Canton,
            //    Parroquia = request.Parroquia,
            //};

            //_context.Customers.Add(customer);
            await _context.SaveChangesAsync(cancellationToken);
            scope.Complete();
        }
            
        return _customerUpdateResponse;
    }
}
