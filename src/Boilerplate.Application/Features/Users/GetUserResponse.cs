using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Application.Features.Users;

public record GetUserResponse
{
    public string Email { get; init; } = null!;

}