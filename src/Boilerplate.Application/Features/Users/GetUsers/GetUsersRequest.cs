using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedList<GetUsersResponse>>
{
    public string? Search { get; init; }
}