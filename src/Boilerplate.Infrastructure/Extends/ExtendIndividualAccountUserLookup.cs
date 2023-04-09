using AuthPermissions.BaseCode.SetupCode;
using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Extends;

public class ExtendIndividualAccountUserLookup : IFindUserInfoService
{
    private readonly UserManager<ApplicationUser> _userManager;

    //
    // Summary:
    //     ctor
    //
    // Parameters:
    //   userManager:
    public ExtendIndividualAccountUserLookup(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    //
    // Summary:
    //     This should find an user in the authentication provider using the AuthPermissions.BaseCode.SetupCode.BulkLoadUserWithRolesTenant.UniqueUserName.
    //     It returns userId and its user name (if no user found with that uniqueName, then
    //
    // Parameters:
    //   uniqueName:
    //
    // Returns:
    //     a class containing a UserIf and UserName property, or null if not found
    public async Task<FindUserInfoResult> FindUserInfoAsync(string uniqueName)
    {
        ApplicationUser identityUser = await _userManager.FindByNameAsync(uniqueName);
        return identityUser == null ? null : new FindUserInfoResult(identityUser.Id.ToString(), identityUser.UserName);
    }
}
