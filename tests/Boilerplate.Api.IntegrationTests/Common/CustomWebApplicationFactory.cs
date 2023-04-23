using Boilerplate.Application.Common;
using Boilerplate.Infrastructure.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Respawn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Boilerplate.Api.IntegrationTests.Common;

public class CustomWebApplicationFactory : WebApplicationFactory<IAssemblyMarker>
{
    private string _connString = default!;
    private Respawner _respawner = default!;
    
    public HttpClient Client { get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            // Replace sql server context for sqlite
            var serviceTypes = new List<Type>
            {
                typeof(DbContextOptions<ApplicationDbContext>),
                typeof(IContext),
            };
            var contextsDescriptor = services.Where(d => serviceTypes.Contains(d.ServiceType)).ToList();
            foreach (var descriptor in contextsDescriptor)
                services.Remove(descriptor);

            //services.AddScoped(_ => CreateContext());
        }).ConfigureLogging(o => o.AddFilter(loglevel => loglevel >= LogLevel.Error));
        base.ConfigureWebHost(builder);
    }
    
    

    public async Task InitializeAsync()
    {
        //await _dbContainer.StartAsync();
        //_connString = $"{_dbContainer.ConnectionString};TrustServerCertificate=True";
        //Client = CreateClient();
        //await using var context = CreateContext();
        ////await context.Database.MigrateAsync();
        //await SetupRespawnerAsync();
    }

    public async Task ResetDatabaseAsync()
    {
        await _respawner.ResetAsync(_connString);
    }

    private async Task SetupRespawnerAsync()
    {
        _respawner = await Respawner.CreateAsync(_connString, new RespawnerOptions
        {
            DbAdapter = DbAdapter.SqlServer,
            SchemasToInclude = new[] {"dbo"}
        });
    }

    //async Task IAsyncLifetime.DisposeAsync() => await _dbContainer.DisposeAsync();
}