using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Customer.CustomerCreate;

public class CustomerCreateHandler : IRequestHandler<CustomerCreateRequest, CustomerCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<CustomerCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private CustomerCreateResponse _customerCreateResponse;


    public CustomerCreateHandler(IContext context, IMapper mapper, ILogger<CustomerCreateHandler> logger, IMailService mail, ICustomerCreateResponse customerCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _customerCreateResponse = (CustomerCreateResponse)customerCreateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
    }
    public async Task<CustomerCreateResponse> Handle(CustomerCreateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var orderNumber = _context.Counters.Where(x => x.Slug == "ORDERSFCME").FirstOrDefault();
                var order = new Order();
                //order.
                
                return _customerCreateResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                //_logger.LogInformation(3, ex.Message);
                //_userResponse.SweetAlert.Title = ex.Message;
                //_userResponse.SweetAlert.Text = ex.Message;
                return _customerCreateResponse;
            }
        }
    }
}
