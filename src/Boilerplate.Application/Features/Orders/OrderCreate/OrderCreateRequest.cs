using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.PaymentMethods;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderCreate;
public class OrderCreateRequest: IRequest<OrderCreateResponse>
{
    public UserAssigned? UserAssigned { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public CustomerCreateRequest CustomerCreateRequest { get; set; } = new CustomerCreateRequest();
    public List<ArticleSearchByPaymentMethodTypeResponse> ArticleSearchResponse { get; set; } = new List<ArticleSearchByPaymentMethodTypeResponse>();
    public List<PaymentMethodByIdResponse> PaymentMethodAllResponse { get; set; } = new List<PaymentMethodByIdResponse>();
}
