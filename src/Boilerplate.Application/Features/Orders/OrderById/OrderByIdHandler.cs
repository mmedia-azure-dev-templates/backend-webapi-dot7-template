using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Users.GetUserById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdHandler : IRequestHandler<OrderByIdRequest, OrderByIdResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public OrderByIdHandler(IContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var orderByIdResponse = new OrderByIdResponse();
        var result = await (from order in _context.Orders.AsNoTracking()
                            join orderItems in _context.OrderItems.AsNoTracking() on order.Id equals orderItems.OrderId
                            join articles in _context.Articles.AsNoTracking() on orderItems.ArticleId equals articles.Id
                            join userGeneratedApplicationUser in _context.ApplicationUsers.AsNoTracking() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = userGeneratedApplicationUser.Id }
                            join userGeneratedUserInformation in _context.UserInformations.AsNoTracking() on new { p1 = (Guid)order.UserGenerated } equals new { p1 = (Guid)userGeneratedUserInformation.UserId }
                            join userAssignedApplicationUser in _context.ApplicationUsers.AsNoTracking() on new { p2 = (Guid)order.UserAssigned } equals new { p2 = userAssignedApplicationUser.Id } into j1 from userAssignedApplicationUser in j1.DefaultIfEmpty()
                            join userAssignedUserInformation in _context.UserInformations.AsNoTracking() on new { p2 = (Guid)order.UserAssigned } equals new { p2 = (Guid)userAssignedUserInformation.UserId } into j2 from userAssignedUserInformation in j2.DefaultIfEmpty()
                            join customer in _context.Customers.AsNoTracking() on order.CustomerId equals customer.Id
                            where order.Id == request.OrderId
                            select new { 
                                order, 
                                orderItems, 
                                articles, 
                                userGeneratedApplicationUser, 
                                userGeneratedUserInformation,
                                userAssignedApplicationUser,
                                userAssignedUserInformation,
                                customer 
                            }).ToListAsync(cancellationToken);

        List<ArticleSearchResponse> articleSearchResponse = new List<ArticleSearchResponse>();
        foreach (var item in result)
        {
            var articleSearch = new ArticleSearchResponse();
            articleSearch.Id = item.articles.Id;
            articleSearch.DataKey = item.articles.DataKey;
            articleSearch.Provider = item.articles.Provider;
            articleSearch.Sku = item.articles.Sku;
            articleSearch.Abrevia = item.articles.Abrevia;
            articleSearch.Display = item.articles.Display;
            articleSearch.Quantity = item.orderItems.Quantity;
            articleSearch.Cost = item.articles.Cost;
            articleSearch.Total = item.orderItems.Total;
            articleSearch.Brand = item.articles.Brand;
            articleSearch.Notes = item.articles.Notes;
            articleSearch.Meta = item.articles.Meta;
            articleSearch.Discontinued = item.articles.Discontinued;
            articleSearch.IsSelected = true;
            articleSearchResponse.Add(articleSearch);
        }

        var getUsersResponse = new GetUsersResponse();
        var getUserAssignedApplicationUser = result.Select(x => x.userAssignedApplicationUser).FirstOrDefault();
        var getUserAssignedUserInformation = result.Select(x => x.userAssignedUserInformation).FirstOrDefault();

        getUsersResponse = getUserAssignedApplicationUser != null && getUserAssignedUserInformation != null ? _mapper.Map<GetUsersResponse>((getUserAssignedApplicationUser, getUserAssignedUserInformation)) : null;

        orderByIdResponse.Order = result.Select(x => x.order).FirstOrDefault();
        orderByIdResponse.ArticleSearchResponse = articleSearchResponse;
        orderByIdResponse.UserGenerated = _mapper.Map<GetUsersResponse>((result.Select(x => x.userGeneratedApplicationUser).FirstOrDefault(), result.Select(x => x.userGeneratedUserInformation).FirstOrDefault()));
        orderByIdResponse.UserAssigned = getUsersResponse;
        orderByIdResponse.Customer = result.Select(x => x.customer).FirstOrDefault();
        return orderByIdResponse;
    }
}