using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.Forgot;
public record ForgotPasswordRequest: IRequest<ForgotPasswordResponse>
{
    public string Email { get; init; } = null!;
}
