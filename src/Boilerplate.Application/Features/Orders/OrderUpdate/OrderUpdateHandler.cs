using Amazon.Runtime.Documents;
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
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

namespace Boilerplate.Application.Features.Orders.OrderUpdate;

public class OrderUpdateHandler : IRequestHandler<OrderUpdateRequest, OrderUpdateResponse>
{
    private readonly IContext _context;
    private readonly ISession _session;
    private readonly IMapper _mapper;
    private readonly ILogger<OrderUpdateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IPdfService _pdfService;
    private OrderUpdateResponse _orderUpdateResponse;


    public OrderUpdateHandler(IContext context, ISession session, IMapper mapper, ILogger<OrderUpdateHandler> logger, IMailService mail, IOrderUpdateResponse orderUpdateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service, IPdfService pdfService)
    {
        _logger = logger;
        _mapper = mapper;
        _context = context;
        _session = session;
        _mail = mail;
        _orderUpdateResponse = (OrderUpdateResponse)orderUpdateResponse;
        _localizationService = localizationService;
        _awsS3Service = awsS3Service;
        _pdfService = pdfService;
    }
    public async Task<OrderUpdateResponse> Handle(OrderUpdateRequest request, CancellationToken cancellationToken)
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

                var order = await _context.Orders.Where(x => x.Id == request.OrderId).FirstOrDefaultAsync(cancellationToken);
                order = _mapper.Map(request, order);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync(cancellationToken);

                //List<OrderItem> orderItems = new List<OrderItem>();
                //foreach (var article in request.ArticleSearchResponse)
                //{
                //    var item = new OrderItem
                //    {
                //        OrderId = order.Id,
                //        ArticleId = article.Id,
                //        Quantity = article.Quantity,
                //        Price = article.Cost,
                //        Total = article.Total,
                //    };
                //    orderItems.Add(item);
                //}
                //await _context.OrderItems.AddRangeAsync(orderItems);
                //await _context.SaveChangesAsync(cancellationToken);

                //_pdfService.GenerateOrderPdf(order, orderItems, customer);

                scope.Complete();
                _orderUpdateResponse.Message = "Orden Guardada Correctamente";
                return _orderUpdateResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                //_logger.LogInformation(3, ex.Message);
                //_userResponse.SweetAlert.Title = ex.Message;
                //_userResponse.SweetAlert.Text = ex.Message;
                _orderUpdateResponse.Message = ex.Message;
                return _orderUpdateResponse;
            }
        }
    }
}
