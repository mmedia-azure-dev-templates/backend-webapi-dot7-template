using MediatR;
using OneOf;

namespace Boilerplate.Application.Features.Auth.Authenticate;

public record AuthenticateRequest : IRequest<AuthenticateResponse>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;
}