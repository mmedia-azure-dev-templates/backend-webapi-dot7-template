﻿using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using MediatR;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedList<GetUsersResponse>>
{
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
}