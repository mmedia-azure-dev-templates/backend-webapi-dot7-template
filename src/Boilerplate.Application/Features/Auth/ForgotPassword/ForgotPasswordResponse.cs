using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using Boilerplate.Domain.Services;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public class ForgotPasswordResponse: IForgotPasswordResponse
{
    public ISweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;

    public ForgotPasswordResponse(ISweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}