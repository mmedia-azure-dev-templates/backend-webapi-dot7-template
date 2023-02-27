using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Auth.ResetPassword;
public class ResetPasswordResponse : IResetPasswordResponse
{
    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;
    public ResetPasswordResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}
