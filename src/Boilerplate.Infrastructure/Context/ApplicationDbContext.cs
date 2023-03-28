// https://www.johnaclee.com/blog/automatic-created-and-updated-dates-with-entity-framework
using AuthPermissions.AspNetCore.GetDataKeyCode;
using AuthPermissions.BaseCode.CommonCode;
using AuthPermissions.BaseCode.DataLayer.EfCode;
using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Implementations;
using Boilerplate.Infrastructure.Configuration;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Boilerplate.Infrastructure.Context;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IContext, IDataKeyFilterReadOnly
{
    public string DataKey { get; }
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IGetDataKeyFromUser dataKeyFilter) : base(options) {
        // The DataKey is null when: no one is logged in, its a background service, or user hasn't got an assigned tenant
        // In these cases its best to set the data key that doesn't match any possible DataKey 
        DataKey = dataKeyFilter?.DataKey ?? "Bad key";
    }

    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public virtual DbSet<UserInformation> UserInformations { get; set; }
    public virtual DbSet<GeographicLocation> GeographicLocations { get; set; }
    public virtual DbSet<Team> Teams { get; set; }
    public virtual DbSet<Article> Articles { get; set; }
    public virtual DbSet<Counter> Counters { get; set; }
    public virtual DbSet<OrderItem> OrderItems { get; set; }
    public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Hero> Heroes { get; set; }
    public DbSet<CompanyTenant> Companies { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<LineItem> LineItems { get; set; }
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.MarkWithDataKeyIfNeeded(DataKey);
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override int SaveChanges()
    {
        SetProperties();
        return base.SaveChanges();
    }

    private void SetProperties()
    {
        foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Added))
        {
            var created = entity.Entity as IDateCreated;
            if (created != null)
            {
                created.DateCreated = DateTime.Now;
            }
        }

        foreach (var entity in ChangeTracker.Entries().Where(p => p.State == EntityState.Modified))
        {
            var updated = entity.Entity as IDateUpdated;
            if (updated != null)
            {
                updated.DateUpdated = DateTime.Now;
            }
        }
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default(CancellationToken))
    {
        SetProperties();
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
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserInformationConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GeographicLocationConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CounterConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OrderItemConfiguration).Assembly);
    }
}