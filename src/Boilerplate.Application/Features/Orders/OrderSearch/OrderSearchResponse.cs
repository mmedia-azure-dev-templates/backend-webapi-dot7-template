using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.Customers.CustomerById;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Application.Features.Payments.PaymentById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderSearch;
public class OrderSearchResponse
{
    public Order Order { get; set; }
    public PaymentMethodByIdResponse? PaymentMethod { get; set; }
    public List<PaymentByIdResponse> ListPayments { get; set; }
    public List<ArticleSearchByPaymentMethodTypeResponse> ListArticleSearchResponse { get; set; }
    public GetUsersResponse? UserGenerated { get; set; }
    public GetUsersResponse? UserAssigned { get; set; }
    public CustomerByIdResponse? CustomerByIdResponse { get; set; }
}
