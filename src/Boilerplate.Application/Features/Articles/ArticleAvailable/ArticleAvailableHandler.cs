using AuthPermissions.AdminCode;
using Boilerplate.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Articles.ArticleAvailable;
public class ArticleAvailableHandler : IRequestHandler<ArticleAvailableRequest, ArticleAvailableResponse>
{
    private readonly IContext _context;
    private readonly IAuthUsersAdminService _authUsersAdminService;
    public ArticleAvailableHandler(IContext context, IAuthUsersAdminService authUsersAdmin)
    {
        _context = context;
        _authUsersAdminService = authUsersAdmin;
    }

    public async Task<ArticleAvailableResponse> Handle(ArticleAvailableRequest request, CancellationToken cancellationToken)
    {
        ArticleAvailableResponse articleAvailableResponse = new ArticleAvailableResponse();
        articleAvailableResponse.Message = "Article is not available";

        var article = await _context.Articles.Where(x => x.Sku == request.Sku).FirstOrDefaultAsync();
        if (article == null)
        {
            articleAvailableResponse.IsAvailable = true;
            articleAvailableResponse.Message = "Article is available";

        }

        return articleAvailableResponse;
    }
}
