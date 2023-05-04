//https://stackoverflow.com/questions/33153932/filter-search-using-multiple-fields-asp-net-mvc
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleSearch;
public class ArticleSearchHandler : IRequestHandler<ArticleSearchRequest, PaginatedList<ArticleSearchResponse>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public ArticleSearchHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<ArticleSearchResponse>> Handle(ArticleSearchRequest request, CancellationToken cancellationToken)
    {

        var articles = from article in _context.Articles.AsNoTracking().DefaultIfEmpty()
                       select article;

        var defaultFilter = articles;

        var articlesItemsMethods = from article in defaultFilter.AsNoTracking().DefaultIfEmpty()
                            join articleItem in _context.ArticlesItems.AsNoTracking() on article.Id equals articleItem.ArticleId into j1
                            from articleItem in j1.DefaultIfEmpty()
                            join paymentMethod in _context.PaymentMethods.AsNoTracking() on articleItem.PaymentMethodId equals paymentMethod.Id into j2
                            from paymentMethod in j2.DefaultIfEmpty()
                                   select new ArticleItemPaymentMethodResponse
                                   {
                                       ArticleItemId = articleItem.Id,
                                       PaymentMethodId = articleItem.PaymentMethodId,
                                       PaymentMethodsType = paymentMethod.PaymentMethodsType,
                                       Display = paymentMethod.Display,
                                       Price = articleItem.Price,
                                       DateCreated = articleItem.DateCreated,
                                       DateUpdated = articleItem.DateUpdated
                                   };


        var result = from article in defaultFilter.AsNoTracking().DefaultIfEmpty()
                     orderby article.Sku ascending
                     select new ArticleSearchResponse
                     {
                         Article = article,
                         ListArticleItemPaymentMethodResponse = (List<ArticleItemPaymentMethodResponse>)(
                                                   articlesItemsMethods)
                     };

        return await result.ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
