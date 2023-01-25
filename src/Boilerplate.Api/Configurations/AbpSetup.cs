using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Data;
using Volo.Abp.Emailing;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;

namespace Boilerplate.Api.Configurations;

[DependsOn(typeof(AbpEmailingModule))]
[DependsOn(typeof(AbpMultiTenancyModule))]
public class AbpSetup : AbpModule
{
    public AbpSetup()
    {
        SkipAutoServiceRegistration = true;
    }
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAssemblyOf<AbpSetup>();
        var configuration = context.Services.GetConfiguration();
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = connectionString;
        });        
    }
}
