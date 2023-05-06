using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Articles.ArticleCreate;
public class ArticleCreateHandler:IRequestHandler<ArticleCreateRequest, ArticleCreateResponse>
{
    private readonly IContext _context;

    public ArticleCreateHandler(IContext context)
    {
        _context = context;
    }
    public async Task<ArticleCreateResponse> Handle(ArticleCreateRequest request, CancellationToken cancellationToken)
    {
        ArticleCreateResponse articleCreateResponse = new ArticleCreateResponse();
        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            Article article = new()
            {
                Provider = request.Provider,
                Sku = request.Sku,
                Display = request.Display,
                Brand = request.Brand,
                Notes = request.Notes,
                Meta = request.Meta,
                Discontinued = request.Discontinued,
            };

            await _context.Articles.AddAsync(article);

            List<ArticleItem> listArticlesItems = new List<ArticleItem>();
            foreach (var articleItem in request.ListArticleItemsPrices)
            {
                if(articleItem.Price != null)
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

            articleCreateResponse.Article = article;
            articleCreateResponse.ListArticlesItems = listArticlesItems;
            articleCreateResponse.Message = "Article created successfully!";
            await _context.SaveChangesAsync(cancellationToken);
            scope.Complete();
        }

        return articleCreateResponse;
    }
}
