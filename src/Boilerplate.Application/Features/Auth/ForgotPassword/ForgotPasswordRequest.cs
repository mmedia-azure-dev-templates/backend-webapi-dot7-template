using Boilerplate.Domain.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public record ForgotPasswordRequest : IRequest<IForgotPasswordResponse>
{
    public string Email { get; init; } = null!;
}
