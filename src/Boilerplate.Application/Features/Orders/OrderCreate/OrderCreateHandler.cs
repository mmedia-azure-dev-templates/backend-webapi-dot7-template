using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Emails;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Orders.OrderCreate;

public class OrderCreateHandler : IRequestHandler<OrderCreateRequest, OrderCreateResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private OrderCreateResponse _orderCreateResponse;


    public OrderCreateHandler(IContext context, IMapper mapper, ILogger<OrderCreateHandler> logger, IMailService mail, IOrderCreateResponse orderCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _mail = mail;
        _orderCreateResponse = (OrderCreateResponse)orderCreateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
    }
    public async Task<OrderCreateResponse> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var orderNumber = _context.Counters.Where(x => x.Slug == "ORDERSFCME").FirstOrDefault();
                var order = new Order();
                //order.
                
                return _orderCreateResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                //_logger.LogInformation(3, ex.Message);
                //_userResponse.SweetAlert.Title = ex.Message;
                //_userResponse.SweetAlert.Text = ex.Message;
                return _orderCreateResponse;
            }
        }
    }
}
