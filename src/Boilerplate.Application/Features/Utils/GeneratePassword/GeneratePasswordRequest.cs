using MediatR;

namespace Boilerplate.Application.Features.Utils.GeneratePassword;

public record GeneratePasswordRequest : IRequest<GeneratePasswordResponse>
{
    public string Password { get; set; } = string.Empty;
}