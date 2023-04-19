using Boilerplate.Application.Features.Payments.PaymentById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentController : ControllerBase
{
    private readonly IMediator _mediator;
    public PaymentController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /// <summary>
    /// Returns payment by orderId
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaymentByIdResponse), StatusCodes.Status200OK)]
    [HttpGet]
    [Route("getpaymentbyid")]
    public async Task<PaymentByIdResponse> CreateArticle([FromQuery]PaymentByIdRequest request)
    {
        return await _mediator.Send(request);
    }
}
