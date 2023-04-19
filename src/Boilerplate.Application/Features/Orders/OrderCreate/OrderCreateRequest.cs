using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.PaymentMethods;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
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
    public CustomerCreateRequest CustomerCreateRequest { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
    public List<PaymentMethodAllResponse> PaymentMethodAllResponse { get; set; }
}
