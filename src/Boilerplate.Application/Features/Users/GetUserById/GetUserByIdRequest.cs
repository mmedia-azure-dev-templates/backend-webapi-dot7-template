using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByIdRequest:IRequest<GetUserByIdResponse> {

    public Guid Id { get; init; }
    public GetUserByIdRequest(Guid id)
    {
        Id = id;
    }
    //(UserId Id) : IRequest<OneOf<UserResponse, UserNotFound>>;
}