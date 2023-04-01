using AuthPermissions.SupportCode.AddUsersServices;

namespace Boilerplate.Application.Features.TenantAdmin.VerifyInvitation;
public class VerifyInvitationResponse
{
    public bool IsValid { get; set; } = false;
    public string Message { get; set; }
    public AddNewUserDto addNewUserDto { get; set; }
}