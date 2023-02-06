using MediatR;

namespace Boilerplate.Application.Features.Users.Migration;
public class UsersMigrationRequest : IRequest<UsersMigrationResponse>
{
    public string PasswordMigration
    {
        get; set;
    }
}
