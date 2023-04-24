using Amazon.S3.Model;
using Boilerplate.Application.Common.Requests;
using Boilerplate.Application.Common.Responses;
using Boilerplate.Domain.Entities.Enums;
using MediatR;
using System.Collections.Generic;

namespace Boilerplate.Application.Features.Users.GetUsers;

public class GetUsersRequest : PaginatedRequest, IRequest<PaginatedList<GetUsersResponse>>
{
    public OwnerFilterType Filter { get; set; }
    public string Search { get; init; } = null!;
}