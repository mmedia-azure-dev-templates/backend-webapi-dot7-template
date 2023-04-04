﻿using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderUpdate;
public class OrderUpdateRequest: IRequest<OrderUpdateResponse>
{
    public OrderId OrderId { get; set; }
    public PaymentMethodsType PaymentMethodsType { get; set; }
    public UserAssigned? UserAssigned { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Total { get; set; }
    public string? Notes { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public CustomerCreateRequest CustomerCreateRequest { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
}