using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdRequest:IRequest<GetUserByIdResponse> {

    public UserId Id { get; init; }
    public GetUserByIdRequest(UserId id)
    {
        Id = id;
    }
    //(UserId Id) : IRequest<OneOf<UserResponse, UserNotFound>>;
}