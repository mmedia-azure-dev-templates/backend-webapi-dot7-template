using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
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
    private readonly ILogger<OrderCreateHandler> _logger;
    private readonly IMailService _mail;
    private readonly ILocalizationService _localizationService;
    private readonly IAwsS3Service _awsS3Service;
    private readonly IPdfService _pdfService;
    private OrderCreateResponse _orderCreateResponse;


    public OrderCreateHandler(IContext context,ISession session, IMapper mapper, ILogger<OrderCreateHandler> logger, IMailService mail, IOrderCreateResponse orderCreateResponse, ILocalizationService localizationService, IAwsS3Service awsS3Service, IPdfService pdfService)
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
                var customer = new Customer
                {
                    DocumentType = request.CustomerCreateRequest.DocumentType,
                    Ndocument = request.CustomerCreateRequest.Ndocument,
                    BirthDate = request.CustomerCreateRequest.BirthDate,
                    GenderType = request.CustomerCreateRequest.GenderType,
                    CivilStatusType = request.CustomerCreateRequest.CivilStatusType,
                    FirstName = request.CustomerCreateRequest.FirstName,
                    LastName = request.CustomerCreateRequest.LastName,
                    Email = request.CustomerCreateRequest.Email,
                    Mobile = request.CustomerCreateRequest.Mobile,
                    Phone = request.CustomerCreateRequest.Phone,
                    PrimaryStreet = request.CustomerCreateRequest.PrimaryStreet,
                    SecondaryStreet = request.CustomerCreateRequest.SecondaryStreet,
                    Numeration = request.CustomerCreateRequest.Numeration,
                    Reference = request.CustomerCreateRequest.Reference,
                    Provincia = request.CustomerCreateRequest.Provincia,
                    Canton = request.CustomerCreateRequest.Canton,
                    Parroquia = request.CustomerCreateRequest.Parroquia,
                    Notes = request.CustomerCreateRequest.Notes,
                };
                await _context.Customers.AddAsync(customer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                var counter = _context.Counters.Where(x => x.Slug == "ORDERSFCME").FirstOrDefault();
                counter.CustomCounter = counter!.CustomCounter.Value + 1;
                await _context.SaveChangesAsync(cancellationToken);


                var order = new Order
                {
                    OrderStatusType = OrderStatusType.Entered,
                    OrderNumber = counter.CustomCounter.Value,
                    UserGenerated = _session.UserId.Value,
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
                _orderCreateResponse.Message = "Orden Creada Correctamente";
                return _orderCreateResponse;
            }
            catch (Exception ex)
            {
                //List<IdentityError> errorList = result.Errors.ToList();
                //var errors = string.Join(" | ", errorList.Select(e => e.Description));
                //_logger.LogInformation(3, ex.Message);
                //_userResponse.SweetAlert.Title = ex.Message;
                //_userResponse.SweetAlert.Text = ex.Message;
                _orderCreateResponse.Message = ex.Message;
                return _orderCreateResponse;
            }
        }
    }
}
