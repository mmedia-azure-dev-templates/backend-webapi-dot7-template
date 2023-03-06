using AuthPermissions.AdminCode;
using Boilerplate.Application.Common;
using Boilerplate.Application.Features.Users.CreateUser;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.Users.AvailableUserEmail;
public class AvailableUserEmailHandler : IRequestHandler<AvailableUserEmailRequest, AvailableUserEmailResponse>
{
    private readonly IContext _context;
    private readonly IAuthUsersAdminService _authUsersAdminService;
    public AvailableUserEmailHandler(IContext context, IAuthUsersAdminService authUsersAdmin)
    {
        _context = context;
        _authUsersAdminService = authUsersAdmin;
    }

    public async Task<AvailableUserEmailResponse> Handle(AvailableUserEmailRequest request, CancellationToken cancellationToken)
    {
        AvailableUserEmailResponse availableUserResponse = new AvailableUserEmailResponse();
        availableUserResponse.IsAvailable = true;
        availableUserResponse.Message = "Email is available";

        var userEmail = await _context.ApplicationUsers.Where(x => x.Email == request.EmailAddress).FirstOrDefaultAsync();
        var authpEmail = await _authUsersAdminService.FindAuthUserByEmailAsync(request.EmailAddress.ToString());
        if (userEmail != null)
        {
            availableUserResponse.IsAvailable = false;
            availableUserResponse.Message = "Identity Email already exists";

        }

        if (authpEmail.IsValid)
        {
            availableUserResponse.IsAvailable = false;
            availableUserResponse.Message = "Authp Email already exists";
        }

        return availableUserResponse;
    }
}
