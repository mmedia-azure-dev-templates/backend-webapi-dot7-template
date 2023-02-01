using Boilerplate.Application.Common.Responses;

namespace Boilerplate.Application.Features.Auth;

public record AuthNotFound : NotFound {
    public string Message { get; init; } = null!;
}