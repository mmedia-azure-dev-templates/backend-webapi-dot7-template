using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Articles.ArticleAvailable;
using Boilerplate.Application.Features.Articles.ArticleById;
using Boilerplate.Application.Features.Articles.ArticleCreate;
using Boilerplate.Application.Features.Articles.ArticleSearch;
using Boilerplate.Application.Features.Articles.ArticleSearchByPaymentMethodType;
using Boilerplate.Application.Features.Articles.ArticleUpdate;
using Boilerplate.Application.Features.Users.AvailableUserEmail;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
    /// Returns all articles by PaymentMethodType
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginatedList<ArticleSearchByPaymentMethodTypeResponse>), StatusCodes.Status200OK)]
    [HttpGet]
    [Route("articlesearchsbypaymentmethodtype")]
    public async Task<PaginatedList<ArticleSearchByPaymentMethodTypeResponse>> GetArticlesSearchByPaymentMethodType([FromQuery] ArticleSearchByPaymentMethodTypeRequest request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// Returns all articles by Filter
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginatedList<ArticleSearchResponse>), StatusCodes.Status200OK)]
    [HttpGet]
    [Route("getarticles")]
    public async Task<PaginatedList<ArticleSearchResponse>> GetArticles([FromQuery] ArticleSearchRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("available")]
    public async Task<ArticleAvailableResponse> Available(ArticleAvailableRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<ArticleCreateResponse>> CreateArticle(ArticleCreateRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpGet]
    [Route("getarticle")]
    public async Task<ArticleByIdResponse> GetArticle([FromQuery]ArticleByIdRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPut]
    [Route("update")]
    public async Task<ArticleUpdateResponse> UpdateArticle(ArticleUpdateRequest request)
    {
        return await _mediator.Send(request);
    }
}
