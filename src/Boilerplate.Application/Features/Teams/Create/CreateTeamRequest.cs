using MediatR;
using System;

namespace Boilerplate.Application.Features.Teams.Create;

public record CreateTeamRequest : IRequest<CreateTeamResponse>
{
    public string Email { get; init; } = null!;  
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}