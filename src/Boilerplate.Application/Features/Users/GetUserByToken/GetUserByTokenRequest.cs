using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;
using System;

namespace Boilerplate.Application.Features.Users.GetUserById;

public class GetUserByTokenRequest:IRequest<GetUserByTokenResponse> {

}