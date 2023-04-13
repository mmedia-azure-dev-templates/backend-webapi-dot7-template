using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Customers.CustomerById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Orders.OrderSearch;
public class OrderSearchResponse
{
    public Order Order { get; set; }
    public List<ArticleSearchResponse> ArticleSearchResponse { get; set; }
    public GetUsersResponse? UserGenerated { get; set; }
    public GetUsersResponse? UserAssigned { get; set; }
    public CustomerByIdResponse? CustomerByIdResponse { get; set; }
}
