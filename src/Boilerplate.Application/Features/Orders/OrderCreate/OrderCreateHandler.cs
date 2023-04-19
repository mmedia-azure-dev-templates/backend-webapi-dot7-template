using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Address.AddresUpdate;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.Customers.CustomerUpdate;
using Boilerplate.Application.Implementations;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Orders.OrderCreate;

public class OrderCreateHandler : IRequestHandler<OrderCreateRequest, OrderCreateResponse>
{
    private readonly IContext _context;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly ILogger<OrderCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IPdfService _pdfService;
    private OrderCreateResponse _orderCreateResponse;


    public OrderCreateHandler(IContext context, ISession session, IMapper mapper, IMediator mediator, ILogger<OrderCreateHandler> logger, IMailService mail, IOrderCreateResponse orderCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service, IPdfService pdfService)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _session = session;
        _mail = mail;
        _orderCreateResponse = (OrderCreateResponse)orderCreateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
        _pdfService = pdfService;
        _mediator = mediator;
    }
    public async Task<OrderCreateResponse> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                CustomerUpdateResponse customerUpdateResponse = new CustomerUpdateResponse();
                CustomerCreateResponse customerCreateResponse = new CustomerCreateResponse();
                CustomerUpdateRequest customerUpdateRequest = new CustomerUpdateRequest();

                if (request.CustomerCreateRequest?.Ndocument != null)
                {

                    var customer = await _context.Customers.Where(x => x.Ndocument == request.CustomerCreateRequest.Ndocument).AsNoTracking().FirstOrDefaultAsync(cancellationToken);

                    if (customer != null)
                    {
                        customerUpdateRequest = _mapper.Map<CustomerUpdateRequest>(customer);
                        customerUpdateRequest.AddresUpdateRequest = _mapper.Map<AddressUpdateRequest>(request.CustomerCreateRequest.addresCreateRequest);
                        customerUpdateRequest.CustomerId = customer.Id;
                        customerUpdateRequest.AddresUpdateRequest.PersonId = new PersonId((Guid)customer.Id);
                        customerUpdateResponse = await _mediator.Send(customerUpdateRequest, cancellationToken);
                    }

                    if (customer == null)
                    {
                        customerCreateResponse = await _mediator.Send(request.CustomerCreateRequest, cancellationToken);
                    }

                }
                var customerId = new CustomerId();

                var counter = _context.Counters.Where(x => x.Slug == "ORDERSFCME").FirstOrDefault();
                counter!.CustomCounter = new CustomCounter(counter!.CustomCounter.Value + 1);
                await _context.SaveChangesAsync(cancellationToken);

                if (customerCreateResponse?.CustomerId != null)
                {
                    customerId = new CustomerId((Guid)customerCreateResponse.CustomerId);
                }

                if (customerUpdateResponse?.CustomerId != null)
                {
                    customerId = new CustomerId((Guid)customerUpdateResponse.CustomerId);
                }

                var order = new Order
                {
                    OrderStatusType = OrderStatusType.Entered,
                    OrderNumber = new OrderNumber(counter.CustomCounter.Value),
                    UserGenerated = new UserGenerated(_session.UserId.Value),
                    UserAssigned = request.UserAssigned,
                    CustomerId = customerId == default ? null : customerId,
                    Notes = request.Notes,
                    SubTotal = request.SubTotal,
                    Total = request.Total,
                };

                await _context.Orders.AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                List<OrderItem> orderItems = new List<OrderItem>();
                foreach (var article in request.ArticleSearchResponse)
                {
                    var item = new OrderItem
                    {
                        OrderId = order.Id,
                        ArticleId = article.ArticleId,
                        Delivered = article.Delivered,
                        Quantity = article.Quantity,
                        Price = article.Cost,
                        Total = article.Total,
                    };
                    orderItems.Add(item);
                }
                await _context.OrderItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync(cancellationToken);

                List<Payment> payments = new List<Payment>();
                foreach (var payment in request.PaymentMethodAllResponse)
                {
                    var item = new Payment
                    {
                        OrderId = order.Id,
                        PaymentMethodId = payment.Id,
                    };
                    payments.Add(item);
                }
                await _context.Payments.AddRangeAsync(payments);
                await _context.SaveChangesAsync(cancellationToken);
                scope.Complete();
                _orderCreateResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("OrderCreatedSuccess").Value;
                _orderCreateResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("OrderCreatedSuccess").Value;
                _orderCreateResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
                _orderCreateResponse.Transaction = true;
                _orderCreateResponse.OrderNumber = order.OrderNumber;
                return _orderCreateResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                //_logger.LogInformation(3, ex.Message);
                //_userResponse.SweetAlert.Title = ex.Message;
                //_userResponse.SweetAlert.Text = ex.Message;
                _logger.LogInformation(3, ex.Message);
                _orderCreateResponse.SweetAlert.Title = ex.Message;
                _orderCreateResponse.SweetAlert.Text = ex.Message;
                _orderCreateResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconError").Value);
                return _orderCreateResponse;
            }
        }
    }
}
