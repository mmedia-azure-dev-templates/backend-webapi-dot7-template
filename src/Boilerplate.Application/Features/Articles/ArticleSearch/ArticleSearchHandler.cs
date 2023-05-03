//https://stackoverflow.com/questions/33153932/filter-search-using-multiple-fields-asp-net-mvc
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static QuestPDF.Helpers.Colors;

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
                       join articleItem in _context.ArticlesItems.AsNoTracking() on article.Id equals articleItem.ArticleId into j1
                       from articleItem in j1.DefaultIfEmpty()
                       join paymentMethod in _context.PaymentMethods.AsNoTracking() on articleItem.PaymentMethodId equals paymentMethod.Id into j2
                       from paymentMethod in j2.DefaultIfEmpty()
                       select new
                       {
                           article,
                           articleItem,
                           paymentMethod
                       };

        var defaultFilter = articles;

        defaultFilter = defaultFilter.Where(x => x.paymentMethod.PaymentMethodsType == request.PaymentMethodsTypePriority);

        if (!string.IsNullOrEmpty(request.Sku))
        {
            defaultFilter = defaultFilter.Where(x => EF.Functions.Like(x.article.Sku, $"%{request.Sku}%"));
        }

        //.WhereIf(!string.IsNullOrEmpty(request.Sku), x => EF.Functions.Like(x.Sku, $"%{request.Sku}%"))
        //.WhereIf(!string.IsNullOrEmpty(request.Display), x => EF.Functions.Like(x.Display!, $"%{request.Display}%"))
        //;

        var result = from item in defaultFilter
                     select new ArticleSearchResponse
                     {
                         ArticleId = item.article.Id,
                        //OrderId = item.
                        //Delivered
                        Provider = item.article.Provider,
                        Sku = item.article.Sku,
                        Display = item.article.Display,
                        //Quantity
                        Cost = item.articleItem.Price,
                        //Total
                        Brand = item.article.Brand,
                        Notes = item.article.Notes,
                        Meta = item.article.Meta,
                        Discontinued = item.article.Discontinued,
                        //IsSelected = item.
                     };

        return await result.OrderBy(x => x.Sku).ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
