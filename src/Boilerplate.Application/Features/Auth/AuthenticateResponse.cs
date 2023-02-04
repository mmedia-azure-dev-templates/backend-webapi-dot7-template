using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth;
public record AuthenticateResponse
{
    public string Token { get; init; } = null!;
}
