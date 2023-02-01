using Boilerplate.Application.Features.Auth;
using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Augh.Authenticate;

public record AuthenticateRequest : IRequest<GetAuthenticateResponse>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; }  = null!;
}