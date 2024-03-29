﻿using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.Customers.CustomerUpdate;
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

namespace Boilerplate.Application.Features.Orders.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateRequest, OrderUpdateResponse>
{
    private readonly IMediator _mediator;
    private readonly IContext _context;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderUpdateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private OrderUpdateResponse _orderUpdateResponse;


    public OrderUpdateHandler(IMediator mediator, IContext context, ISession session, IMapper mapper, ILogger<OrderUpdateHandler> logger, IMailService mail, IOrderUpdateResponse orderUpdateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service)
    {
        _mediator = mediator;
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _session = session;
        _mail = mail;
        _orderUpdateResponse = (OrderUpdateResponse)orderUpdateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
    }
    public async Task<OrderUpdateResponse> Handle(OrderUpdateRequest request, CancellationToken cancellationToken)
    {
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                CustomerUpdateResponse customerUpdateResponse = new CustomerUpdateResponse();
                CustomerCreateResponse customerCreateResponse = new CustomerCreateResponse();
                CustomerUpdateRequest customerUpdateRequest = new CustomerUpdateRequest();
                CustomerCreateRequest customerCreateRequest = new CustomerCreateRequest();
                var customer = await _context.Customers.Where(x => x.Ndocument == request.CustomerUpdateRequest.Ndocument).FirstOrDefaultAsync(cancellationToken);
                if (customer != null)
                {
                    customerUpdateRequest = _mapper.Map<CustomerUpdateRequest>(customer);
                    customerUpdateRequest = request.CustomerUpdateRequest;
                    customerUpdateRequest.CustomerId = customer.Id;
                    customerUpdateRequest.AddresUpdateRequest = request.CustomerUpdateRequest.AddresUpdateRequest;
                    customerUpdateRequest.AddresUpdateRequest.PersonId = new PersonId((Guid)customer.Id);
                    customerUpdateResponse = await _mediator.Send(customerUpdateRequest, cancellationToken);
                }

                if (customer == null && request.CustomerUpdateRequest.Ndocument != null && request.CustomerUpdateRequest.DocumentType != null)
                {
                    customerCreateRequest = _mapper.Map<CustomerCreateRequest>(request.CustomerUpdateRequest);
                    customerCreateResponse = await _mediator.Send(customerCreateRequest, cancellationToken);
                }

                var customerId = new CustomerId();

                if (customerCreateResponse?.CustomerId != null)
                {
                    customerId = new CustomerId((Guid)customerCreateResponse.CustomerId);
                }

                if (customerUpdateResponse?.CustomerId != null)
                {
                    customerId = new CustomerId((Guid)customerUpdateResponse.CustomerId);
                }

                var order = await _context.Orders.Where(x => x.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken);
                order = _mapper.Map(request, order);
                order!.UserGenerated = new UserGenerated((Guid)order.UserGenerated);
                order.PaymentMethodId = request.PaymentMethodId;
                order.CustomerId = customerId == default ? null : customerId;
                order.UserAssigned = request.UserAssigned == null ? null : new UserAssigned((Guid)request.UserAssigned);
                order.Notes = request.Notes;
                _context.Orders.Update(order);
                await _context.SaveChangesAsync(cancellationToken);

                var removeOrderItems = await _context.OrderItems.Where(x => x.OrderId == request.OrderId).ToListAsync();
                _context.OrderItems.RemoveRange(removeOrderItems);
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

                var removePayments = await _context.Payments.Where(x => x.OrderId == request.OrderId).ToListAsync(cancellationToken);
                _context.Payments.RemoveRange(removePayments);
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

                bool checkOrderComplete = false;

                if (
                    order.DataKey != null && 
                    order.UserAssigned != null && 
                    order.CustomerId != null && 
                    payments.Count > 0 &&
                    orderItems.Count > 0)
                {
                    if((customerCreateResponse!.CustomerComplete == true || customerUpdateResponse!.CustomerComplete == true) && order.Locked == false)
                    {
                        checkOrderComplete = true;
                        order.Locked = true;
                    }
                }

                _context.Orders.Update(order);
                await _context.SaveChangesAsync(cancellationToken);

                scope.Complete();
                _orderUpdateResponse.SweetAlert.Title = _localizationService.GetLocalizedHtmlString("OrderCreatedSuccess").Value;
                _orderUpdateResponse.SweetAlert.Text = _localizationService.GetLocalizedHtmlString("OrderCreatedSuccess").Value;
                _orderUpdateResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconSuccess").Value);
                _orderUpdateResponse.Transaction = true;
                _orderUpdateResponse.OrderNumber = order.OrderNumber;
                return _orderUpdateResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(3, ex.Message);
                _orderUpdateResponse.SweetAlert.Title = ex.Message;
                _orderUpdateResponse.SweetAlert.Text = ex.Message;
                _orderUpdateResponse.SweetAlert.Icon = (SweetAlertIconType)Enum.Parse(typeof(SweetAlertIconType), _localizationService.GetLocalizedHtmlString("ForgotPasswordResponseIconError").Value);
                return _orderUpdateResponse;
            }
        }
    }
}
