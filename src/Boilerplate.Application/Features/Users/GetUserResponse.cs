using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Application.Features.Users;

public record GetUserResponse
{
    public string Message { get; set; } = "";
    public bool Transaction { get; set; } = false!;
}