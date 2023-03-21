//https://stackoverflow.com/questions/33153932/filter-search-using-multiple-fields-asp-net-mvc
using AutoMapper;
using Boilerplate.Application.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Extensions;
using Boilerplate.Application.Features.Articles.GetArticleById;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.GetArticleByCode;
public class ArticleSearchHandler : IRequestHandler<ArticleSearchRequest, PaginatedList<ArticleSearchResponse>>
{
    private readonly IContext _context;
    private readonly IMapper _mapper;
    public ArticleSearchHandler(IMapper mapper, IContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PaginatedList<ArticleSearchResponse>> Handle(ArticleSearchRequest request, CancellationToken cancellationToken)
    {
        var heroes = _context.Articles
            .WhereIf(!string.IsNullOrEmpty(request.Sku), x => EF.Functions.Like(x.Sku, $"%{request.Sku}%"))
            //.WhereIf(!string.IsNullOrEmpty(request.Display), x => EF.Functions.Like(x.Display!, $"%{request.Display}%"))
            ;
        return await _mapper.ProjectTo<ArticleSearchResponse>(heroes)
            .OrderBy(x => x.Sku)
            .ToPaginatedListAsync(request.CurrentPage, request.PageSize);
    }
}
