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
        var article = await _context.Articles.Where(x => x.Sku == request.Sku).FirstOrDefaultAsync(cancellationToken);
        if (article != null)
        {
            article.Display = request.Display;
            _context.Articles.Update(article);
            articleItemUpdateBySkuResponse.Article = article;

            var articleItemPaymentMethod = await (from articleItem in _context.ArticlesItems
                                                  join paymentMethod in _context.PaymentMethods on articleItem.PaymentMethodId equals paymentMethod.Id into j1
                                                  from paymentMethod in j1.DefaultIfEmpty()
                                                  where articleItem.ArticleId == article.Id && paymentMethod.PaymentMethodsType == request.PaymentMethodsType
                                                  select new { articleItem, paymentMethod }).FirstOrDefaultAsync(cancellationToken);

            if (articleItemPaymentMethod != null)
            {
                articleItemPaymentMethod.articleItem.Price = request.Price;
                articleItemPaymentMethod.articleItem.PaymentMethodId = articleItemPaymentMethod.paymentMethod.Id;
                _context.ArticlesItems.Update(articleItemPaymentMethod.articleItem);
                articleItemUpdateBySkuResponse.ArticleItem = articleItemPaymentMethod.articleItem;
            }
            if (articleItemPaymentMethod == null)
            {
                var paymentMethod = await _context.PaymentMethods.Where(x => x.PaymentMethodsType == request.PaymentMethodsType).FirstOrDefaultAsync(cancellationToken);
                var articleItem = new ArticleItem
                {
                    ArticleId = article.Id,
                    PaymentMethodId = paymentMethod!.Id,
                    Price = request.Price
                };
                await _context.ArticlesItems.AddAsync(articleItem,cancellationToken);
                articleItemUpdateBySkuResponse.ArticleItem = articleItem;
            }
            await _context.SaveChangesAsync(cancellationToken);
        }

        if (article == null)
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
            await _context.ArticlesItems.AddAsync(articleItem, cancellationToken);
            articleItemUpdateBySkuResponse.ArticleItem = articleItem;
            await _context.SaveChangesAsync(cancellationToken);
        }
        return articleItemUpdateBySkuResponse;
    }
}
