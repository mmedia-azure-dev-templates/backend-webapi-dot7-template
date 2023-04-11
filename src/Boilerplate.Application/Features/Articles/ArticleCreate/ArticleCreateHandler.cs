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
    private readonly IMapper _mapper;
    private ArticleCreateResponse _articleCreateResponse;

    public ArticleCreateHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }
    public async Task<ArticleCreateResponse> Handle(ArticleCreateRequest request, CancellationToken cancellationToken)
    {
        Article article = new()
        {
            Provider = request.Provider,
            Sku = request.Sku,
            Abrevia = request.Abrevia,
            Display = request.Display,
            Cost = request.Cost,
            Brand = request.Brand,
            Notes = request.Notes,
            Meta = request.Meta,
            Discontinued = request.Discontinued,
        };

        _context.Articles.Add(article);
        await _context.SaveChangesAsync(cancellationToken);
        return _articleCreateResponse;
    }
}
