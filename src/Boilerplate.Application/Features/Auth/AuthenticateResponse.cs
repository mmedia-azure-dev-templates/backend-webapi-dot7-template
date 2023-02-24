using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;

namespace Boilerplate.Application.Features.Auth;
public record AuthenticateResponse:IAuthenticateResponse
{
    public string Token { get; set; } = "";

    public SweetAlert SweetAlert { get; set; }
    public bool Transaction { get; set; } = false!;

    public AuthenticateResponse(SweetAlert sweetAlert)
    {
        SweetAlert = sweetAlert;
    }
}
