﻿using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;

namespace Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;

public class ArticleSearchByPaymentMethodTypeRequest : PaginatedRequest, IRequest<PaginatedList<ArticleSearchByPaymentMethodTypeResponse>>
{
    public PaymentMethodsType PaymentMethodsTypePriority { get; set; }
    public string? Sku { get; set; }
    public string? Display { get; set; }
    public int? Brand { get; set; }
}
