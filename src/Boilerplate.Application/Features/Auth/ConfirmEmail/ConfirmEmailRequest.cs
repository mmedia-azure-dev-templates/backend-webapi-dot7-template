using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Auth.ConfirmEmail;
public class ConfirmEmailRequest : IRequest<ConfirmEmailResponse>
{
    public string Token { get; set; } = "";
    public string Email { get; set; } = "";
}
