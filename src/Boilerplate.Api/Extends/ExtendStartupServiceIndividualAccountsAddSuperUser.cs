using Boilerplate.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RunMethodsSequentially;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Boilerplate.Api.Extends;
internal static class ConfigHelper
{
    public static (string email, string password) GetSuperUserConfigData(this IServiceProvider serviceProvider)
    {
        var config = serviceProvider.GetRequiredService<IConfiguration>();
        var superSection = config.GetSection("SuperAdmin");
        if (superSection == null)
            return (null, null);

        return (superSection["Email"], superSection["Password"]);
    }
}

public class StartupServiceIndividualAccountsAddSuperUser<TIdentityUser> : IStartupServiceToRunSequentially
        where TIdentityUser : ApplicationUser, new()
{
    /// <summary>
    /// This must be after migrations, and after the adding demo users startup service.
    /// </summary>
    public int OrderNum { get; } = -1;

    /// <summary>
    /// This will ensure that a user who's email/password is held in the "SuperAdmin" section of 
    /// the appsettings.json file is in the individual users account authentication database
    /// </summary>
    /// <param name="scopedServices">This should be a scoped service</param>
    /// <returns></returns>
    public async ValueTask ApplyYourChangeAsync(IServiceProvider scopedServices)
    {
        var userManager = scopedServices.GetRequiredService<UserManager<TIdentityUser>>();

        var (email, password) = scopedServices.GetSuperUserConfigData();
        if (!string.IsNullOrEmpty(email))
            await CheckAddNewUserAsync(userManager, email, password);
    }

    /// <summary>
    /// This will add a user with the given email if they don't all ready exist
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private static async Task CheckAddNewUserAsync(UserManager<TIdentityUser> userManager, string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user != null)
            return;

        user = new TIdentityUser
        {
            UserName = email,
            Email = email,
            FirstName = "Raúl",
            LastName = "Flores",
            LastLogin = DateTime.Now
        };
        var result = await userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            var errorDescriptions = string.Join("\n", result.Errors.Select(x => x.Description));
            throw new InvalidOperationException(
                $"Tried to add user {email}, but failed. Errors:\n {errorDescriptions}");
        }
    }
}
