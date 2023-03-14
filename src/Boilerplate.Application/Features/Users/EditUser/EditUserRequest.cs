using Boilerplate.Domain.Entities.Common;
using MediatR;

namespace Boilerplate.Application.Features.Users.EditUser;

public record EditUserRequest : IRequest<EditUserResponse>
{
    public UserId UserId { get; set; }
}