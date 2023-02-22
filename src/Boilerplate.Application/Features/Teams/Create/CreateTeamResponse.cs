using Boilerplate.Domain.Entities.Common;

namespace Boilerplate.Application.Features.Teams.Create;

public record CreateTeamResponse
{
    public string Message { get; set; } = "";
    public bool Transaction { get; set; } = false!;
}