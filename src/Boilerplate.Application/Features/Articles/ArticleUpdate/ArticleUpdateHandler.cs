using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

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
        return _articleUpdateResponse;
    }
}
