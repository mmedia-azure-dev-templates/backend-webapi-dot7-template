//https://stackoverflow.com/questions/33153932/filter-search-using-multiple-fields-asp-net-mvc
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Orders.OrderById;
using Boilerplate.Application.Features.Users.GetUsers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using org.apache.zookeeper.data;
using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Application.Features.Orders.OrderSearch;
public class OrderSearchHandler : IRequestHandler<OrderSearchRequest, PaginatedList<OrderSearchResponse>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public OrderSearchHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<OrderSearchResponse>> Handle(OrderSearchRequest request, CancellationToken cancellationToken)
    {
        var orderByIdResponse = new OrderByIdResponse();
        var result = (from order in _context.Orders.AsNoTracking().DefaultIfEmpty()
                      join orderItems in _context.OrderItems.AsNoTracking().DefaultIfEmpty() on order.Id equals orderItems.OrderId into j1
                      from orderItems in j1.DefaultIfEmpty()
                      join articles in _context.Articles.AsNoTracking().DefaultIfEmpty() on orderItems.ArticleId equals articles.Id into j2
                      from articles in j2.DefaultIfEmpty()
                      join userGeneratedApplicationUser in _context.ApplicationUsers.AsNoTracking().DefaultIfEmpty() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = userGeneratedApplicationUser.Id } into j3
                      from userGeneratedApplicationUser in j3.DefaultIfEmpty()
                      join userGeneratedUserInformation in _context.UserInformations.AsNoTracking().DefaultIfEmpty() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = (Guid)userGeneratedUserInformation.UserId } into j4
                      from userGeneratedUserInformation in j4.DefaultIfEmpty()
                      join userAssignedApplicationUser in _context.ApplicationUsers.AsNoTracking() on (Guid?)order.UserAssigned equals userAssignedApplicationUser.Id into j5
                      from userAssignedApplicationUser in j5.DefaultIfEmpty()
                      join userAssignedUserInformation in _context.UserInformations.AsNoTracking() on (Guid?)order.UserAssigned equals (Guid)userAssignedUserInformation.UserId into j6
                      from userAssignedUserInformation in j6.DefaultIfEmpty()
                      join customer in _context.Customers.AsNoTracking().DefaultIfEmpty() on order.CustomerId equals customer.Id
                      where order.DateCreated <= DateTime.Now
                      orderby order.OrderNumber ascending
                      select new
                      {
                          order,
                          orderItems,
                          articles,
                          userGeneratedApplicationUser,
                          userGeneratedUserInformation,
                          userAssignedApplicationUser,
                          userAssignedUserInformation,
                          customer
                      });//.ToListAsync(cancellationToken);

        //var orders = result.GroupBy(x => x.order.OrderNumber);//.Select(x => x.First());
        //var products = result.GroupBy(x => x.orderItems.Id).Select(x => x.First());

        var mierda = await (from order in result.Select(x=>x.order)
                            group order by order.OrderNumber into g
                            select new { PersonId = g.Key, Cars = g.ToList() }).ToListAsync(cancellationToken);

        //    //.Select(x => x.First().order).ToList();
        //var userGeneratedApplicationUser = result.GroupBy(x => x.userGeneratedApplicationUser).Select(x => x.First().userGeneratedApplicationUser).ToList();
        //var userGeneratedUserInformation = result.GroupBy(x => x.userGeneratedApplicationUser).Select(x => x.First().userGeneratedApplicationUser).ToList();
        //List<OrderByIdResponse> listOrderByIdResponse = new List<OrderByIdResponse>();
        //foreach (var order in orders)
        //{
        //    var temp = new OrderByIdResponse();
        //    temp.Order = order.order;
        //    temp.Customer = order.customer;
        //    temp.UserGenerated = _mapper.Map<GetUsersResponse>((order.userGeneratedApplicationUser, order.userGeneratedUserInformation));
        //    temp.UserAssigned = _mapper.Map<GetUsersResponse>((order.userGeneratedApplicationUser, order.userGeneratedUserInformation));
        //    listOrderByIdResponse.Add(temp);
        //}

        throw new NotImplementedException();
        //return await _mapper.ProjectTo<OrderSearchResponse>(result).OrderBy(x => x.).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
        //var orders = result.GroupBy(x => x.order.OrderNumber).Select(x => x.First().order).ToList();
        //List<ArticleSearchResponse> articleSearchResponse = new List<ArticleSearchResponse>();
        //foreach (var item in result)
        //{
        //    //Replace OrderItems with ArticleSearchResponse
        //    var articleSearch = new ArticleSearchResponse();
        //    articleSearch.Id = item.articles.Id;
        //    articleSearch.DataKey = item.articles.DataKey;
        //    articleSearch.Provider = item.articles.Provider;
        //    articleSearch.Sku = item.articles.Sku;
        //    articleSearch.Abrevia = item.articles.Abrevia;
        //    articleSearch.Display = item.articles.Display;
        //    articleSearch.Quantity = item.orderItems.Quantity;
        //    articleSearch.Cost = item.articles.Cost;
        //    articleSearch.Total = item.orderItems.Total;
        //    articleSearch.Brand = item.articles.Brand;
        //    articleSearch.Notes = item.articles.Notes;
        //    articleSearch.Meta = item.articles.Meta;
        //    articleSearch.Discontinued = item.articles.Discontinued;
        //    articleSearch.IsSelected = true;
        //    articleSearchResponse.Add(articleSearch);
        //}

        //var orderSearchResponse = new OrderSearchResponse();

        //var getUsersResponse = new GetUsersResponse();
        //var getUserAssignedApplicationUser = result.Select(x => x.userAssignedApplicationUser).FirstOrDefault();
        //var getUserAssignedUserInformation = result.Select(x => x.userAssignedUserInformation).FirstOrDefault();

        //getUsersResponse = getUserAssignedApplicationUser != null && getUserAssignedUserInformation != null ? _mapper.Map<GetUsersResponse>((getUserAssignedApplicationUser, getUserAssignedUserInformation)) : null;

        //orderByIdResponse.Order = result.Select(x => x.order).FirstOrDefault();
        //orderByIdResponse.ArticleSearchResponse = articleSearchResponse;
        //orderByIdResponse.UserGenerated = _mapper.Map<GetUsersResponse>((result.Select(x => x.userGeneratedApplicationUser).FirstOrDefault(), result.Select(x => x.userGeneratedUserInformation).FirstOrDefault()));
        //orderByIdResponse.UserAssigned = getUsersResponse;
        //orderByIdResponse.Customer = result.Select(x => x.customer).FirstOrDefault();

        ////var articles = _context.Articles
        ////    .WhereIf(!string.IsNullOrEmpty(request.Sku), x => EF.Functions.Like(x.Sku, $"%{request.Sku}%"))
        ////    //.WhereIf(!string.IsNullOrEmpty(request.Display), x => EF.Functions.Like(x.Display!, $"%{request.Display}%"))
        ////    ;


    }
}
