using AuthPermissions.AspNetCore.GetDataKeyCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.EfCode;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IContext, IDataKeyFilterReadOnly
{
    public string DataKey { get; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IGetDataKeyFromUser dataKeyFilter) : base(options) {
        // The DataKey is null when: no one is logged in, its a background service, or user hasn't got an assigned tenant
        // In these cases its best to set the data key that doesn't match any possible DataKey 
        DataKey = dataKeyFilter?.DataKey ?? "Bad key";
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<UserInformation> UserInformations { get; set; }
    public virtual DbSet<Hero> Heroes { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public DbSet<CompanyTenant> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.MarkWithDataKeyIfNeeded(DataKey);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        this.MarkWithDataKeyIfNeeded(DataKey);
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseExceptionProcessor();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        //modelBuilder.HasDefaultSchema("web");
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            if (typeof(IDataKeyFilterReadWrite).IsAssignableFrom(entityType.ClrType))
            {
                entityType.AddSingleTenantReadWriteQueryFilter(this);
            }
            else
            {
                //throw new Exception(
                //    $"You haven't added the {nameof(IDataKeyFilterReadWrite)} to the entity {entityType.ClrType.Name}");
            }

            foreach (var mutableProperty in entityType.GetProperties())
            {
                if (mutableProperty.ClrType == typeof(decimal))
                {
                    mutableProperty.SetPrecision(9);
                    mutableProperty.SetScale(2);
                }
            }
        }

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationUserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserInformationConfiguration).Assembly);
    }
}