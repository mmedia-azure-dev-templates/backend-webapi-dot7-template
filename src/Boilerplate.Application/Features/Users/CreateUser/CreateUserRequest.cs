using Boilerplate.Application.Features.Identications;
using MediatR;

namespace Boilerplate.Application.Features.Users.CreateUser;

public record CreateUserRequest : IRequest<GetUserResponse>
{
    public string Email { get; init; } = null!;  
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}

public record CreateUsersIdenticationsRequest:IRequest<GetUserResponse>
{
    public CreateUserRequest User { get; }
    public CreateIdentificationRequest Identification { get; }
}