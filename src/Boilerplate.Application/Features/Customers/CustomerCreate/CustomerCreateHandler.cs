using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
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
        var customer = new Customer
        {
            DocumentType = request.DocumentType,
            Ndocument = request.Ndocument,
            BirthDate = request.BirthDate,
            GenderType = request.GenderType,
            CivilStatusType = request.CivilStatusType,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Mobile = request.Mobile,
            Phone = request.Phone,
            PrimaryStreet = request.PrimaryStreet,
            SecondaryStreet = request.SecondaryStreet,
            Numeration = request.Numeration,
            Reference = request.Reference,
            Provincia = request.Provincia,
            Canton = request.Canton,
            Parroquia = request.Parroquia,
            Notes = request.Notes,
        };

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync(cancellationToken);

        return _customerCreateResponse;
    }
}
