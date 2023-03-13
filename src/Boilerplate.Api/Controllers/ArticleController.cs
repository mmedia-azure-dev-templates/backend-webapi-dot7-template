using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Application.Features.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;
using Boilerplate.Application.Features.Articles.GetArticleById;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    [Route("article")]
    public async Task<PaginatedList<ArticleSearchResponse>> GetUsers([FromQuery] ArticleSearchRequest request)
    {
        return await _mediator.Send(request);
    }
}
