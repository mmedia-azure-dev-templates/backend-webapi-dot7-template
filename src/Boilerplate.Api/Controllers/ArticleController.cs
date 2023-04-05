using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Application.Features.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Microsoft.AspNetCore.Authorization;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ArticleController : ControllerBase
{
    private readonly IMediator _mediator;
    public ArticleController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Returns all articles by code
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginatedList<ArticleSearchResponse>), StatusCodes.Status200OK)]
    [HttpGet]
    [Route("articles")]
    public async Task<PaginatedList<ArticleSearchResponse>> GetArticles([FromQuery] ArticleSearchRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<ArticleCreateResponse>> CreateArticle(ArticleCreateRequest request)
    {
        return await _mediator.Send(request);
    }
}
