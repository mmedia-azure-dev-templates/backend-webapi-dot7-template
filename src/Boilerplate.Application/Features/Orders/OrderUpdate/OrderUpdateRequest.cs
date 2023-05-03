using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.Customers.CustomerUpdate;
using Boilerplate.Application.Features.PaymentMethods;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderUpdate;
public class OrderUpdateRequest: IRequest<OrderUpdateResponse>
{
    public OrderId OrderId { get; set; }
    public PaymentMethodsType? PaymentMethodsType { get; set; }
    public UserAssigned? UserAssigned { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public CustomerUpdateRequest CustomerUpdateRequest { get; set; } = new CustomerUpdateRequest();
    public List<ArticleSearchByPaymentMethodTypeResponse> ArticleSearchResponse { get; set; } = new List<ArticleSearchByPaymentMethodTypeResponse>();
    public List<PaymentMethodAllResponse> PaymentMethodAllResponse { get; set; } = new List<PaymentMethodAllResponse>();
}
