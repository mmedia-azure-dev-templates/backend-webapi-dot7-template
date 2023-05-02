using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        ArticleCreateResponse _articleCreateResponse = new ArticleCreateResponse();
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

        _context.Articles.Add(article);
        await _context.SaveChangesAsync(cancellationToken);
        _articleCreateResponse.Article = article;
        return _articleCreateResponse;
    }
}
