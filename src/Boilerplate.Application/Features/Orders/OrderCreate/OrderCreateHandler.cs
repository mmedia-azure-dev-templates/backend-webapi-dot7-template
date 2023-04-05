using Amazon.Runtime.Documents;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Orders.OrderCreate;

public class OrderCreateHandler : IRequestHandler<OrderCreateRequest, OrderCreateResponse>
{
    private readonly IContext _context;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IPdfService _pdfService;
    private OrderCreateResponse _orderCreateResponse;


    public OrderCreateHandler(IContext context, ISession session, IMapper mapper, ILogger<OrderCreateHandler> logger, IMailService mail, IOrderCreateResponse orderCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service, IPdfService pdfService)
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
    }
    public async Task<OrderCreateResponse> Handle(OrderCreateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var customer = await _context.Customers.Where(x => x.Ndocument == request.CustomerCreateRequest.Ndocument).FirstOrDefaultAsync(cancellationToken);

                if (customer != null)
                {
                   _context.Customers.Update(customer);
                }

                if (customer == null)
                {
                    customer = _mapper.Map(request.CustomerCreateRequest, customer);
                    await _context.Customers.AddAsync(customer, cancellationToken);
                }

                await _context.SaveChangesAsync(cancellationToken);

                var counter = _context.Counters.Where(x => x.Slug == "ORDERSFCME").FirstOrDefault();
                counter.CustomCounter = counter!.CustomCounter.Value + 1;
                await _context.SaveChangesAsync(cancellationToken);


                var order = new Order
                {
                    OrderStatusType = OrderStatusType.Entered,
                    OrderNumber = counter.CustomCounter.Value,
                    UserGenerated = new UserGenerated(_session.UserId.Value),
                    UserAssigned = request.UserAssigned,
                    CustomerId = customer.Id,
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
                        ArticleId = article.Id,
                        Quantity = article.Quantity,
                        Price = article.Cost,
                        Total = article.Total,
                    };
                    orderItems.Add(item);
                }
                await _context.OrderItems.AddRangeAsync(orderItems);
                await _context.SaveChangesAsync(cancellationToken);

                //_pdfService.GenerateOrderPdf(order, orderItems, customer);

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
