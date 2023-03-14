using AuthPermissions;
using AuthPermissions.AspNetCore;
using AuthPermissions.AspNetCore.JwtTokenCode;
using Boilerplate.Api.Common;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.AvailableUserDocument;
using Boilerplate.Application.Features.Users.AvailableUserEmail;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Application.Features.Users.DeleteUser;
using Boilerplate.Application.Features.Users.EditUser;
using Boilerplate.Application.Features.Users.GetUserById;
using Boilerplate.Application.Features.Users.GetUserByToken;
using Boilerplate.Application.Features.Users.GetUsers;
using Boilerplate.Application.Features.Users.Migration;
using Boilerplate.Application.Features.Users.UpdatePassword;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.PermissionsCode;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ISession = Boilerplate.Domain.Implementations.ISession;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("create")]
    public async Task<ActionResult<UserResponse>> CreateUser(CreateUsersInformationsRequest request)
    {
        UserResponse response = await _mediator.Send(request);
        return response.Transaction == false ? Ok(response) : Created("", response);
    }

    [HasPermission(DefaultPermissions.UserChange)]
    [HttpPut]
    [Route("edit")]
    public async Task<EditUserResponse> Edit([FromBody]EditUserRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [Route("migrate-password")]
    public async Task<ActionResult<UsersMigrationResponse>> MigratePassword(UsersMigrationRequest request)
    {
        return await _mediator.Send(request);
    }


    /// <summary>
    /// Returns all users in the database
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [ProducesResponseType(typeof(PaginatedList<UserResponse>), StatusCodes.Status200OK)]
    //[Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("getusers")]
    public async Task<ActionResult<PaginatedList<UserResponse>>> GetUsers([FromQuery] GetUsersRequest request)
    {
        return Ok(await _mediator.Send(request));
    }


    /// <summary>
    /// Get one user by id from the database
    /// </summary>
    /// <returns></returns>
    //[Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("getuserbytoken")]
    [ProducesResponseType(typeof(GetUserByTokenResponse), StatusCodes.Status200OK)]
    public async Task<GetUserByTokenResponse> GetUser()
    {
        return await _mediator.Send(new GetUserByTokenRequest());
    }

    /// <summary>
    /// Get one user by id from the database
    /// </summary>
    /// <param name="request">The user's ID</param>
    /// <returns></returns>
    //[Authorize(Roles = Roles.Admin)]
    [HttpGet]
    [Route("getuserbyid")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(GetUserByIdResponse), StatusCodes.Status200OK)]
    public async Task<GetUserByIdResponse> GetUser([FromQuery]GetUserByIdRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPatch("password")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordRequest request)
    {            
        await _mediator.Send(request with { Id = _session.UserId });
        return NoContent();
    }

    [HttpPost("availableemail")]
    [AllowAnonymous]
    public async Task<AvailableUserEmailResponse> AvailableEmail(AvailableUserEmailRequest request)
    {
        return await _mediator.Send(request);
    }

    [HttpPost("availabledocument")]
    [AllowAnonymous]
    public async Task<AvailableUserNdocumentResponse> AvailableDocument(AvailableUserNdocumentRequest request)
    {
        return await _mediator.Send(request);
    }

    //[Authorize(Roles = Roles.Admin)]
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