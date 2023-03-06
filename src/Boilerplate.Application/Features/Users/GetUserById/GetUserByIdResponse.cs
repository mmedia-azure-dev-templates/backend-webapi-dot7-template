using Boilerplate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.GetUserById;
public class GetUserByIdResponse
{
    public ApplicationUser applicationUser { get; set; }
    public UserInformation userInformation { get; set; }
}
