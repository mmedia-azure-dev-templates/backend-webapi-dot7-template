using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Auth.Authenticate;

public record AuthenticateRequest : IRequest<OneOf<AuthenticateResponse, AuthenticateNotFound>>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;
}