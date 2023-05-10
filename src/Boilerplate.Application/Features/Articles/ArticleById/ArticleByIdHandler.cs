using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Articles.ArticleUpdate;
using Boilerplate.Application.Features.PaymentMethods.PaymentMethodById;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using org.apache.zookeeper.data;
using System.Drawing;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleById;
public class ArticleByIdHandler : IRequestHandler<ArticleByIdRequest, ArticleByIdResponse>
{
    private readonly IContext _context;

    public ArticleByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<ArticleByIdResponse> Handle(ArticleByIdRequest request, CancellationToken cancellationToken)
    {
        var articles = from article in _context.Articles.AsNoTracking().DefaultIfEmpty()
                       where article.Id == request.ArticleId
                       select article;

        var defaultFilter = articles;

        var articlesItemsMethods = from article in defaultFilter.AsNoTracking().DefaultIfEmpty()
                                   join articleItem in _context.ArticlesItems.AsNoTracking() on article.Id equals articleItem.ArticleId into j1
                                   from articleItem in j1.DefaultIfEmpty()
                                   join paymentMethod in _context.PaymentMethods.AsNoTracking() on articleItem.PaymentMethodId equals paymentMethod.Id into j2
                                   from paymentMethod in j2.DefaultIfEmpty()
                                   select new
                                   {
                                       articleItem,
                                       paymentMethod
                                   };

        var result = from article in defaultFilter.AsNoTracking().DefaultIfEmpty()
                     select new ArticleByIdResponse
                     {
                         Article = article,
                         ListArticleItem = articlesItemsMethods.GroupBy(x=>x.articleItem.Id).Select(x => x.First().articleItem).ToList(),
                         ListPaymentMethodByIdResponse = articlesItemsMethods.GroupBy(x =>x.paymentMethod.Id).Select(x => new PaymentMethodByIdResponse
                         {
                             Id = x.First().paymentMethod.Id,
                             DataKey = x.First().paymentMethod.DataKey,
                             PaymentMethodsType = x.First().paymentMethod.PaymentMethodsType,
                             Display = x.First().paymentMethod.Display,
                             Priority = x.First().paymentMethod.Priority,
                             Active = x.First().paymentMethod.Active,
                             Icon = x.First().paymentMethod.Icon,
                             IsSelected = true,
                             DateCreated = x.First().paymentMethod.DateCreated,
                             DateUpdated = x.First().paymentMethod.DateUpdated
                         }).ToList()
                     };

        return await result.FirstOrDefaultAsync(cancellationToken);
    }
}
