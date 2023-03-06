using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.AvailableUserEmail;
public class AvailableUserEmailResponse
{
    public bool IsAvailable { get; set; }
    public string Message { get; set; }
}
