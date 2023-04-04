using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Orders.OrderById;
public class OrderByIdHandler : IRequestHandler<OrderByIdRequest, OrderByIdResponse>
{
    public readonly IContext _context;
    public OrderByIdHandler(IContext context)
    {
        _context = context;
    }

    public async Task<OrderByIdResponse> Handle(OrderByIdRequest request, CancellationToken cancellationToken)
    {
        var orderByIdResponse = new OrderByIdResponse();
        var result = await (from order in _context.Orders.AsNoTracking()
                            join orderItems in _context.OrderItems.AsNoTracking() on order.Id equals orderItems.OrderId
                            join articles in _context.Articles.AsNoTracking() on orderItems.ArticleId equals articles.Id
                            //new { p1 = q.QOT_SEC_ID, p2 = dpr.DPR_TS } equals new { p1 = (decimal)p.PAY_SEC_ID, p2 = p.PAY_DATE }
                            join userGeneratedUserInformation in _context.UserInformations.AsNoTracking() on new { p1 = order.UserGenerated } equals new { p1 = new UserGenerated(new Guid(userGeneratedUserInformation.UserId.ToString())) } into PersonasColegio
                            from pco in PersonasColegio.DefaultIfEmpty()
                            join customer in _context.Customers.AsNoTracking() on order.CustomerId equals customer.Id
                            where order.Id == request.OrderId
                            select new { order, orderItems, articles, pco, customer }).ToListAsync(cancellationToken);

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

        var ownerUser = new GetUsersResponse();


        orderByIdResponse.Order = result.Select(x => x.order).FirstOrDefault();
        orderByIdResponse.ArticleSearchResponse = articleSearchResponse;
        orderByIdResponse.Customer = result.Select(x => x.customer).FirstOrDefault();
        return orderByIdResponse;
    }
}