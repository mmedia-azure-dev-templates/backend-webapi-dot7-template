using Boilerplate.Application.Features.Customers.CustomerById;
using Boilerplate.Application.Features.Customers.CustomerCreate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Boilerplate.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CustomerController : ControllerBase
{
    private readonly IMediator _mediator;
    public CustomerController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("create")]
    public async Task<CustomerCreateResponse> Create(CustomerCreateRequest request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// Get one customer by Id from the database
    /// </summary>
    /// <param name="request">The customer's Id</param>
    /// <returns></returns>
    [HttpGet]
    [Route("customerbyid")]
    [ProducesResponseType(typeof(CustomerByIdResponse), StatusCodes.Status200OK)]
    public async Task<CustomerByIdResponse> CustomerById([FromQuery] CustomerByIdRequest request)
    {
        return await _mediator.Send(request);
    }

}
