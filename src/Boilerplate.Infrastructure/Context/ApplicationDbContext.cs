using Boilerplate.Application.Common;
using Boilerplate.Domain.Entities;
using Boilerplate.Infrastructure.Configuration;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Context;

public class ApplicationDbContext : DbContext, IContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public virtual DbSet<Article> Articles { get; set; }
    
    public virtual DbSet<Catalog> Catalogs { get; set; }
    
    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<GeographicLocation> GeographicLocations { get; set; }

    public virtual DbSet<Hero> Heroes { get; set; }

    public virtual DbSet<Identification> Identifications { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }
    
    public virtual DbSet<InventoryDoc> InventoryDocs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }
    
    public virtual DbSet<Postulant> Postulants { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.UseExceptionProcessor();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("web");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentificationConfiguration).Assembly);
        
    }
}