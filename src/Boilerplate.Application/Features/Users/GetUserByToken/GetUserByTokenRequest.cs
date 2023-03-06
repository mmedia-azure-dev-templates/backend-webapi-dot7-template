using Boilerplate.Domain.Entities.Common;
using MediatR;
using OneOf;
using System;

namespace Boilerplate.Application.Features.Users.GetUserByToken;

public class GetUserByTokenRequest : IRequest<GetUserByTokenResponse>
{

}