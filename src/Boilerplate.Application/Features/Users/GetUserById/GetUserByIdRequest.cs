using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdRequest:IRequest<GetUserByIdResponse> {
    public UserId UserId { get; set; }

    public GetUserByIdRequest(UserId userId)
    {
        UserId = userId;
    }
}