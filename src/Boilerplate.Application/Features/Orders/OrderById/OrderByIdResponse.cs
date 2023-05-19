using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.Customers.CustomerById;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Application.Features.Payments.PaymentById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdResponse: IOrderByIdResponse
{
    public Order Order { get; set; } = new();
    public PaymentMethodByIdResponse? PaymentMethodById { get; set; }
    public List<PaymentByIdResponse> ListPaymentByIdResponse { get; set; }
    public List<ArticleSearchByPaymentMethodTypeResponse> ListArticleSearchResponse { get; set; } = new();
    public GetUsersResponse? UserGenerated { get; set; }
    public GetUsersResponse? UserAssigned { get; set; }
    public CustomerByIdResponse? Customer { get; set; }
}
