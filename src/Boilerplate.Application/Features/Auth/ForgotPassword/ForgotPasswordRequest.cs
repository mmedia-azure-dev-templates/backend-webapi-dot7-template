using Boilerplate.Domain.Implementations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ForgotPassword;
public class ForgotPasswordRequest : IRequest<ForgotPasswordResponse>
{
    public string Email { get; init; } = null!;
}
