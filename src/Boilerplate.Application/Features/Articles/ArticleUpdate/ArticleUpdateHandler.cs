using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Articles.ArticleUpdate;
public class ArticleUpdateHandler:IRequestHandler<ArticleUpdateRequest, ArticleUpdateResponse>
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
            article.Provider = request.Provider;
            article.Sku = request.Sku;
            article.Display = request.Display;
            article.Brand = request.Brand;
            article.Notes = request.Notes;
            article.Meta = request.Meta;
            article.Discontinued = request.Discontinued;

            await _context.Articles.AddAsync(article);

            List<ArticleItem> listArticlesItems = new List<ArticleItem>();
            foreach (var articleItem in request.ListArticleItemsPrices)
            {
                if (articleItem.Price != null)
                {
                    var item = new ArticleItem
                    {
                        ArticleId = article.Id,
                        PaymentMethodId = articleItem.PaymentMethodId,
                        Price = (decimal)articleItem.Price,
                    };
                    listArticlesItems.Add(item);
                }
            }
            await _context.ArticlesItems.AddRangeAsync(listArticlesItems);

            _articleUpdateResponse.Article = article;
            _articleUpdateResponse.ListArticlesItems = listArticlesItems;
            _articleUpdateResponse.Message = "Article updated successfully!";
            await _context.SaveChangesAsync(cancellationToken);
            scope.Complete();
        }

        return _articleUpdateResponse;
    }
}
