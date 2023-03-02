using ISession = Boilerplate.Domain.Implementations.ISession;
using Boilerplate.Application.Features.Utils.GeneratePassword;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MediatR;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UtilController : ControllerBase
{
    private readonly ISession _session;
    private readonly IMediator _mediator;

    public UtilController(
        ISession session,
        IMediator mediator
        )
    {
        _session = session;
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("generate-password")]
    public async Task<ActionResult<GeneratePasswordResponse>> GeneratePassword(GeneratePasswordRequest request)
    {
        return await _mediator.Send(request);
    }

}
