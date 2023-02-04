using AuthPermissions;
using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Application.Features.Users.DeleteUser;
using Boilerplate.Application.Features.Users.GetUserById;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Application.Features.Users.UpdatePassword;
using Boilerplate.Domain.Auth;
using Boilerplate.Domain.Entities.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ISession = Boilerplate.Domain.Auth.Interfaces.ISession;

namespace Boilerplate.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UserController : ControllerBase
{
    private readonly ISession _session;
    private readonly IMediator _mediator;

    public UserController(
        ISession session, 
        IMediator mediator, 
        ITokenBuilder tokenBuilder, 
        IClaimsCalculator claimsCalculator
        )
    {
        _session = session;
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<ActionResult<GetUserResponse>> CreateUser(CreateUsersIdenticationsRequest request)
    {
        return await _mediator.Send(request);
    }

    /// <summary>
    /// Authenticates the user and returns the token information.
    /// </summary>
    /// <param name="request">Email and password information</param>
    /// <returns>Token information</returns>
    [HttpPost]
    [Route("authenticate")]
    [AllowAnonymous]

    /// <summary>
    /// Returns all users in the database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginatedList<GetUserResponse>), StatusCodes.Status200OK)]
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    public async Task<ActionResult<PaginatedList<GetUserResponse>>> GetUsers([FromQuery] GetUsersRequest request)
    {
        return Ok(await _mediator.Send(request));
    }


    /// <summary>
    /// Get one user by id from the database
    /// </summary>
    /// <param name="id">The user's ID</param>
    /// <returns></returns>
    [Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetUserResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserById(UserId id)
    {
        var result = await _mediator.Send(new GetUserByIdRequest(id));
        return result.Match<IActionResult>(
            found => Ok(found),
            notFound => NotFound());
    }

    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {            
        await _mediator.Send(request with { Id = _session.UserId });
        return NoContent();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> DeleteUser(UserId id)
    {
        var result = await _mediator.Send(new DeleteUserRequest(id));
        return result.Match<IActionResult>(
            deleted => NoContent(),
            notFound => NotFound());
    }
}