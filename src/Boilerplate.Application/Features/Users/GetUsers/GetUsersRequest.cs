using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using MediatR;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedList<UserResponse>>
{
    public string? Email { get; init; }
    public bool IsAdmin { get; init; }
}