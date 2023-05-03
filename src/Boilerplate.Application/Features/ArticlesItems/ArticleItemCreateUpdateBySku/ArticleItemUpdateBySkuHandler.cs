using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemCreateUpdateBySku;
public class ArticleItemUpdateBySkuHandler : IRequestHandler<ArticleItemUpdateBySkuRequest, ArticleItemUpdateBySkuResponse>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;

    public ArticleItemUpdateBySkuHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ArticleItemUpdateBySkuResponse> Handle(ArticleItemUpdateBySkuRequest request, CancellationToken cancellationToken)
    {
        ArticleItemUpdateBySkuResponse articleItemUpdateBySkuResponse = new ArticleItemUpdateBySkuResponse();
        var result = await (from article in _context.Articles.DefaultIfEmpty()
                            join articleItem in _context.ArticlesItems on article.Id equals articleItem.ArticleId into j1
                            from articleItem in j1.DefaultIfEmpty()
                            join paymentMethod in _context.PaymentMethods on articleItem.PaymentMethodId equals paymentMethod.Id into j2
                            from paymentMethod in j2.DefaultIfEmpty()
                            where article.Sku == request.Sku
                            select new { article, articleItem }).FirstOrDefaultAsync(cancellationToken);
        if (result != null)
        {
            result.article.Display = request.Display;
            _context.Articles.Update(result.article);
            articleItemUpdateBySkuResponse.Article = result.article;

            if (result.articleItem != null)
            {
                result.articleItem.Price = request.Price;
                _context.ArticlesItems.Update(result.articleItem);
                articleItemUpdateBySkuResponse.ArticleItem = result.articleItem;
            }
            if (result.articleItem == null)
            {
                var paymentMethod = await _context.PaymentMethods.Where(x => x.PaymentMethodsType == request.PaymentMethodsType).FirstOrDefaultAsync(cancellationToken);
                var articleItem = new ArticleItem
                {
                    ArticleId = result.article.Id,
                    PaymentMethodId = paymentMethod!.Id,
                    Price = request.Price
                };
                _context.ArticlesItems.Add(articleItem);
                articleItemUpdateBySkuResponse.ArticleItem = articleItem;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (result == null)
        {
            var articleCreateRequest = new ArticleCreateRequest
            {
                Provider = 1,
                Sku = request.Sku,
                Display = request.Display,
                Brand = 1,
                Notes = null,
                Meta = null,
                Discontinued = false,
            };
            var articleCreateResponse = await new ArticleCreateHandler(_context).Handle(articleCreateRequest, cancellationToken);
            articleItemUpdateBySkuResponse.Article = articleCreateResponse.Article;

            var paymentMethod = await _context.PaymentMethods.Where(x => x.PaymentMethodsType == request.PaymentMethodsType).FirstOrDefaultAsync(cancellationToken);
            var articleItem = new ArticleItem
            {
                ArticleId = articleCreateResponse.Article.Id,
                PaymentMethodId = paymentMethod!.Id,
                Price = request.Price
            };
            _context.ArticlesItems.Add(articleItem);
            articleItemUpdateBySkuResponse.ArticleItem = articleItem;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return articleItemUpdateBySkuResponse;
    }
}
