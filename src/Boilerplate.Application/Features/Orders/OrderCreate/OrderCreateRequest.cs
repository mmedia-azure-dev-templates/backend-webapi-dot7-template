using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Application.Features.OrderItems.OrderItemCreate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using Microsoft.Graph;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderCreate;
public class OrderCreateRequest: IRequest<OrderCreateResponse>
{
    public PaymentMethodsType AgreegmentPaymentType { get; set; }
    public OrderStatusType OrderStatusType { get; set; } = OrderStatusType.Entered;
    public decimal? Credit { get; set; }
    public UserGenerated UserGenerated { get; set; }
    public UserAssigned UserAssigned { get; set; }
    public CustomerId CustomerId { get; set; }
    public decimal CashAdvance { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public decimal Balance { get; set; }
    public int? Term { get; set; }
    public string? Observations { get; set; }
    public string? Notes { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public DateTime? PaidDate { get; set; }
    public UserPaid? UserPaid { get; set; }
    public bool? PaidState { get; set; }
    public string? Dispatch { get; set; }
    public string? Extras { get; set; }
    public CustomerCreateRequest CustomerCreateRequest { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
