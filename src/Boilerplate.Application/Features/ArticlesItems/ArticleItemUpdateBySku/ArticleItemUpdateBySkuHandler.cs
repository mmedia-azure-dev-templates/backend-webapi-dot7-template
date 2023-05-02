using AutoMapper;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.ArticlesItems.ArticleItemUpdateBySku;
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
            articleItemUpdateBySkuResponse.Article = article;
            article.Display = request.Display;
            _context.Articles.Update(article);
            var articleItem = await _context.ArticlesItems.Where(x => x.ArticleId == article.Id).Where(x => x.PaymentMethodId == request.PaymentMethodId).FirstOrDefaultAsync(cancellationToken);
            if(articleItem != null)
            {
                articleItem!.Price = request.Price;
                articleItem.PaymentMethodId = request.PaymentMethodId;
                _context.ArticlesItems.Update(articleItem);
            }
            await _context.SaveChangesAsync(cancellationToken);
        }
        return articleItemUpdateBySkuResponse;
    }
}
