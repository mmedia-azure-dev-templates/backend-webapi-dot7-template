using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Articles.ArticleUpdate;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleById;
public class ArticleByIdHandler : IRequestHandler<ArticleByIdRequest, ArticleSearchResponse>
{
    private readonly IContext _context;

    public ArticleByIdHandler(IContext context)
    {
        _context = context;
    }
    public async Task<ArticleSearchResponse> Handle(ArticleByIdRequest request, CancellationToken cancellationToken)
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
                         ListArticleItemPaymentMethodResponse = (
                                                  from articleItemMethod in articlesItemsMethods
                                                  where articleItemMethod.ArticleId == article.Id
                                                  select articleItemMethod).ToList(),
                     };

        return await result.FirstOrDefaultAsync(cancellationToken);
    }
}
