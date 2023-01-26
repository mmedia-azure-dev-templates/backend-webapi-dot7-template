using AuthPermissions.BaseCode.DataLayer;
using Boilerplate.Domain.ClaimsChangeCode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Net.DistributedFileStoreCache;
using System.Text.Json;

namespace Boilerplate.Api.Configurations;

public static class CacheSetup
{
    public static IServiceCollection AddCacheSetup(this IServiceCollection services, IWebHostEnvironment environment)
    {
        //Register code for updating user's permissions claim when the user's Roles have changed
        services.AddDistributedFileStoreCache(options =>
        {
            options.WhichVersion = FileStoreCacheVersions.Class;
            //makes it easier to look at the content, but makes a update very slightly slower 
            options.JsonSerializerForCacheFile = new JsonSerializerOptions { WriteIndented = true };
            //I override the the default first part of the FileStore cache file because there are many example apps in this repo
            options.FirstPartOfCacheFileName = "Example2CacheFileStore";
        }, environment);
        services.AddScoped<IDatabaseStateChangeEvent, RoleChangedDetectorService>();
        services.AddScoped<IDatabaseStateChangeEvent, EmailChangeDetectorService>();

        return services;
    }
}
