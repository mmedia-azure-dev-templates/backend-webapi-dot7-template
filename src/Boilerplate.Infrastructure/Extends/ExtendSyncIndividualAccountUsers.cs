using AuthPermissions.AdminCode;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Extends;

public class ExtendSyncIndividualAccountUsers : ISyncAuthenticationUsers
{
    private readonly UserManager<ApplicationUser> _userManager;

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="userManager"></param>
    public ExtendSyncIndividualAccountUsers(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    /// <summary>
    /// This returns the userId, email and UserName of all the users
    /// </summary>
    /// <returns>collection of SyncAuthenticationUser</returns>
    public async Task<IEnumerable<SyncAuthenticationUser>> GetAllActiveUserInfoAsync()
    {
        return await _userManager.Users
            .Select(x => new SyncAuthenticationUser(x.Id.ToString(), x.Email, x.UserName)).ToListAsync();
    }
}
