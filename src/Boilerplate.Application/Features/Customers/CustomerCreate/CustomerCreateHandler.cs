using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Addresses.AddressCreate;
using Boilerplate.Application.Features.Customers.CustomerUpdate;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Customers.CustomerCreate;

public class CustomerCreateHandler : IRequestHandler<CustomerCreateRequest, CustomerCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private CustomerCreateResponse _customerCreateResponse;
    private readonly IMediator _mediator;


    public CustomerCreateHandler(IContext context, IMapper mapper, ILogger<CustomerCreateHandler> logger, IMailService mail, ICustomerCreateResponse customerCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service, IMediator mediator)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _customerCreateResponse = (CustomerCreateResponse)customerCreateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
        _mediator = mediator;
    }
    public async Task<CustomerCreateResponse> Handle(CustomerCreateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            AddressCreateRequest addresCreateRequest = new AddressCreateRequest();
            var customer = new Customer();
            customer = _mapper.Map(request, customer);
            customer.Id = new CustomerId(Guid.NewGuid());
            await _context.Customers.AddAsync(customer, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            if (request.addresCreateRequest != null)
            {
                addresCreateRequest = request.addresCreateRequest;
                addresCreateRequest.PersonId = new PersonId((Guid)customer.Id);
                _customerCreateResponse.addresCreateResponse = await _mediator.Send(addresCreateRequest);
            }

            _customerCreateResponse = _mapper.Map(customer, _customerCreateResponse);
            _customerCreateResponse.CustomerId = customer.Id;

            if (
                customer.DataKey != null &&
                customer.DocumentType != null &&
                customer.Ndocument != null &&
                customer.BirthDate != null &&
                customer.GenderType != null &&
                customer.CivilStatusType != null &&
                customer.FirstName != null &&
                customer.LastName != null &&
                customer.Email != null &&
                customer.Mobile != null &&
                _customerCreateResponse.addresCreateResponse?.AddressComplete == true
                )
            {
                _customerCreateResponse.CustomerComplete = true;
            }

            scope.Complete();
        }

        return _customerCreateResponse;
    }
}
