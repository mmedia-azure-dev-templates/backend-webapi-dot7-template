using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using System;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderPdf;
public class OrderPdfResponse
{
    public Order Order { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
    public GetUsersResponse UserGenerated { get; set; }
    public GetUsersResponse? UserAssigned { get; set; }
    public Customer Customer { get; set; }
}
