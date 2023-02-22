using AuthPermissions.AdminCode;
using AuthPermissions.AdminCode.Services;
using Boilerplate.Application.Features.Users;
using Boilerplate.Application.Features.Users.CreateUser;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Boilerplate.Application.Features.Users.UpdateEmail;
public class UpdateEmailHandler : IRequestHandler<UpdateEmailRequest, UpdateEmailResponse>
{
    private readonly ILogger<UpdateEmailHandler> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mail;
    private readonly IAuthUsersAdminService _authUsersAdminService;
    public UpdateEmailHandler(ILogger<UpdateEmailHandler> logger, UserManager<ApplicationUser> userManager, IMailService mail, IAuthUsersAdminService authUsersAdminService)
    {
        _logger = logger;
        _userManager = userManager;
        _mail = mail;
        _authUsersAdminService = authUsersAdminService;
    }

    public async Task<UpdateEmailResponse> Handle(UpdateEmailRequest request, CancellationToken cancellationToken)
    {


        UpdateEmailResponse updateEmailResponse = new UpdateEmailResponse();

        using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user == null)
                {
                    return updateEmailResponse;
                }

                user.UserName = request.EmailReplace;
                user.NormalizedUserName = request.EmailReplace;
                user.Email = request.EmailReplace;
                user.NormalizedEmail = request.EmailReplace;

                await _userManager.UpdateAsync(user);

                var authpUser = await _authUsersAdminService.FindAuthUserByUserIdAsync(user.Id.ToString());

                if (authpUser != null)
                {
                    await _authUsersAdminService.UpdateUserAsync(user.Id.ToString(), request.EmailReplace, request.EmailReplace);
                }

                scope.Complete();
                updateEmailResponse.Transaction = true;
                updateEmailResponse.Message = "Email updated successfully";
                return updateEmailResponse;
            }
            catch (Exception ex)
            {
                _logger.LogInformation(3, ex.Message);
                updateEmailResponse.Message = ex.Message;
                return updateEmailResponse;
            }
        }
    }
}
