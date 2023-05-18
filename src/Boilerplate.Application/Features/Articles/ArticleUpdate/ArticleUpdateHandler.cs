using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Articles.ArticleUpdate;
public class ArticleUpdateHandler : IRequestHandler<ArticleUpdateRequest, ArticleUpdateResponse>
{
    private readonly IContext _context;

    public ArticleUpdateHandler(IContext context)
    {
        _context = context;
    }
    public async Task<ArticleUpdateResponse> Handle(ArticleUpdateRequest request, CancellationToken cancellationToken)
    {
        ArticleUpdateResponse _articleUpdateResponse = new ArticleUpdateResponse();
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            var article = await _context.Articles.Where(x => x.Id == request.ArticleId).FirstOrDefaultAsync(cancellationToken);
            article!.Provider = request.Provider;
            article.Sku = request.Sku;
            article.Display = request.Display;
            article.Brand = request.Brand;
            article.Notes = request.Notes;
            article.Meta = request.Meta;
            article.Discontinued = request.Discontinued;
            _context.Articles.Update(article);

            foreach (var item in request.ListArticleItemsPrices)
            {
                var newArticleItemPrice = await (_context.ArticlesItems.Where(x => x.ArticleId == article.Id && x.PaymentMethodId == item.PaymentMethodId)).FirstOrDefaultAsync(cancellationToken);

                if (newArticleItemPrice != null)
                {
                    newArticleItemPrice.Price = (decimal)item!.Price!;
                    newArticleItemPrice.PaymentMethodId = item!.PaymentMethodId;
                    _context.ArticlesItems.Update(newArticleItemPrice);
                }

                if (newArticleItemPrice == null)
                {
                    newArticleItemPrice = new ArticleItem();
                    newArticleItemPrice.ArticleId = article.Id;
                    newArticleItemPrice.PaymentMethodId = item.PaymentMethodId;
                    newArticleItemPrice.Price = (decimal) item.Price;
                    _context.ArticlesItems.Add(newArticleItemPrice);
                }
            }
            await _context.SaveChangesAsync(cancellationToken);

            var listArticlesItems = await _context.ArticlesItems.Where(x => x.ArticleId == article.Id).ToListAsync(cancellationToken);
            _articleUpdateResponse.Article = article;
            _articleUpdateResponse.ListArticlesItems = listArticlesItems;
            _articleUpdateResponse.Message = "Article updated successfully!";
            scope.Complete();
        }

        return _articleUpdateResponse;
    }
}
