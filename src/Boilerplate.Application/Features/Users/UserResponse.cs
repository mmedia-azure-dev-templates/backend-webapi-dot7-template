using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Users;

public record UserResponse : IUserResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;

    public UserResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}