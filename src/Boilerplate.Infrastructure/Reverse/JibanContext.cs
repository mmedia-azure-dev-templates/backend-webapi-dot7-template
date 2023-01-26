using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Boilerplate.Infrastructure.Reverse;

public partial class JibanContext : DbContext
{
    public JibanContext()
    {
    }

    public JibanContext(DbContextOptions<JibanContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Article> Articles { get; set; }

    public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    public virtual DbSet<AspNetUser> AspNetUsers { get; set; }

    public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }

    public virtual DbSet<AuthPermissionsMigrationHistory> AuthPermissionsMigrationHistories { get; set; }

    public virtual DbSet<AuthUser> AuthUsers { get; set; }

    public virtual DbSet<Catalog> Catalogs { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Counter> Counters { get; set; }

    public virtual DbSet<GeographicLocation> GeographicLocations { get; set; }

    public virtual DbSet<Identification> Identifications { get; set; }

    public virtual DbSet<Inscription> Inscriptions { get; set; }

    public virtual DbSet<InventoryDoc> InventoryDocs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Postulant> Postulants { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<RoleToPermission> RoleToPermissions { get; set; }

    public virtual DbSet<RoleToPermissionsTenant> RoleToPermissionsTenants { get; set; }

    public virtual DbSet<Tenant> Tenants { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserToRole> UserToRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Jiban;User Id=sa;Password=Yourpassword123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Article>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Articles__3214EC07444B98C5");

            entity.ToTable("Articles", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Abrevia)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Cost).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Sku)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AspNetRole>(entity =>
        {
            entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedName] IS NOT NULL)");

            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AspNetRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AspNetUser>(entity =>
        {
            entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

            entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                .IsUnique()
                .HasFilter("([NormalizedUserName] IS NOT NULL)");

            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AspNetUserRole",
                    r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                    });
        });

        modelBuilder.Entity<AspNetUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.ProviderKey).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AspNetUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.Property(e => e.LoginProvider).HasMaxLength(128);
            entity.Property(e => e.Name).HasMaxLength(128);

            entity.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AuthPermissionsMigrationHistory>(entity =>
        {
            entity.HasKey(e => e.MigrationId);

            entity.ToTable("__AuthPermissionsMigrationHistory");

            entity.Property(e => e.MigrationId).HasMaxLength(150);
            entity.Property(e => e.ProductVersion).HasMaxLength(32);
        });

        modelBuilder.Entity<AuthUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("AuthUsers", "authp");

            entity.HasIndex(e => e.Email, "IX_AuthUsers_Email")
                .IsUnique()
                .HasFilter("([Email] IS NOT NULL)");

            entity.HasIndex(e => e.TenantId, "IX_AuthUsers_TenantId");

            entity.HasIndex(e => e.UserName, "IX_AuthUsers_UserName")
                .IsUnique()
                .HasFilter("([UserName] IS NOT NULL)");

            entity.Property(e => e.UserId).HasMaxLength(256);
            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.IsDisabled)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.UserName).HasMaxLength(128);

            entity.HasOne(d => d.Tenant).WithMany(p => p.AuthUsers).HasForeignKey(d => d.TenantId);
        });

        modelBuilder.Entity<Catalog>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Catalogs", "web");

            entity.Property(e => e.CustomData).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Contacts__3214EC076A1489CC");

            entity.ToTable("Contacts", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(400)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CatCivilStatus)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ndocument)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SurName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Counter>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Counters", "web");

            entity.Property(e => e.Slug)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<GeographicLocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Geograph__3214EC0712371339");

            entity.ToTable("GeographicLocation", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Identification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Identifi__3214EC07C6551D2B");

            entity.ToTable("Identifications", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CurriculumUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DepartureDate).HasColumnType("datetime");
            entity.Property(e => e.EntryDate).HasColumnType("datetime");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ndocument)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Notes)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Inscription>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Inscriptions", "web");

            entity.Property(e => e.Agreement)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Information).HasColumnType("text");
            entity.Property(e => e.InscriptionDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<InventoryDoc>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("InventoryDocs", "web");

            entity.Property(e => e.Code).HasColumnType("text");
            entity.Property(e => e.Description).HasColumnType("text");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Orders", "web");

            entity.Property(e => e.Balance).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.CashAdvance).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Credit).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Dispatch)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Documentation).HasColumnType("text");
            entity.Property(e => e.Enterprise)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Extras).HasColumnType("text");
            entity.Property(e => e.GeneratedDate1).HasColumnType("datetime");
            entity.Property(e => e.GeneratedDate2).HasColumnType("datetime");
            entity.Property(e => e.GeneratedHour1).HasColumnType("datetime");
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Iva).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Notes)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Observations).HasColumnType("text");
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.PaidUserType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.PersonType)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.SubTotal).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Total).HasColumnType("decimal(14, 2)");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Payments", "web");

            entity.Property(e => e.Ammount).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Observation).HasColumnType("text");
            entity.Property(e => e.PaidDate).HasColumnType("datetime");
            entity.Property(e => e.Transaction).HasColumnType("text");
        });

        modelBuilder.Entity<Postulant>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Postulan__3214EC0746145958");

            entity.ToTable("Postulants", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.CurriculumUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.ImgUrl)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Mobile)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ndocument)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.SurName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("Products", "web");

            entity.Property(e => e.Brand).HasColumnType("text");
            entity.Property(e => e.Descriptions).HasColumnType("text");
            entity.Property(e => e.Name).HasColumnType("text");
            entity.Property(e => e.Price).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Sku).HasColumnType("text");
            entity.Property(e => e.Total).HasColumnType("decimal(14, 2)");
            entity.Property(e => e.Weigth).HasColumnType("decimal(14, 1)");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(e => e.TokenValue);

            entity.ToTable("RefreshTokens", "authp");

            entity.HasIndex(e => e.AddedDateUtc, "IX_RefreshTokens_AddedDateUtc").IsUnique();

            entity.Property(e => e.TokenValue)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.UserId).HasMaxLength(256);
        });

        modelBuilder.Entity<RoleToPermission>(entity =>
        {
            entity.HasKey(e => e.RoleName);

            entity.ToTable("RoleToPermissions", "authp");

            entity.HasIndex(e => e.RoleType, "IX_RoleToPermissions_RoleType");

            entity.Property(e => e.RoleName).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.RoleType).HasDefaultValueSql("(CONVERT([tinyint],(0)))");
        });

        modelBuilder.Entity<RoleToPermissionsTenant>(entity =>
        {
            entity.HasKey(e => new { e.TenantRolesRoleName, e.TenantsTenantId });

            entity.ToTable("RoleToPermissionsTenant", "authp");

            entity.HasIndex(e => e.TenantsTenantId, "IX_RoleToPermissionsTenant_TenantsTenantId");

            entity.Property(e => e.TenantRolesRoleName).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.TenantRolesRoleNameNavigation).WithMany(p => p.RoleToPermissionsTenants).HasForeignKey(d => d.TenantRolesRoleName);

            entity.HasOne(d => d.TenantsTenant).WithMany(p => p.RoleToPermissionsTenants).HasForeignKey(d => d.TenantsTenantId);
        });

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.ToTable("Tenants", "authp");

            entity.HasIndex(e => e.ParentDataKey, "IX_Tenants_ParentDataKey");

            entity.HasIndex(e => e.ParentTenantId, "IX_Tenants_ParentTenantId");

            entity.HasIndex(e => e.TenantFullName, "IX_Tenants_TenantFullName").IsUnique();

            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();
            entity.Property(e => e.HasOwnDb)
                .IsRequired()
                .HasDefaultValueSql("(CONVERT([bit],(0)))");
            entity.Property(e => e.ParentDataKey)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.TenantFullName).HasMaxLength(400);

            entity.HasOne(d => d.ParentTenant).WithMany(p => p.InverseParentTenant).HasForeignKey(d => d.ParentTenantId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC077E5B4E11");

            entity.ToTable("Users", "web");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(60)
                .IsUnicode(false);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastLoginIp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Password).HasColumnType("text");
            entity.Property(e => e.RememberToken)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasColumnType("text");
            entity.Property(e => e.SurName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserToRole>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleName });

            entity.ToTable("UserToRoles", "authp");

            entity.HasIndex(e => e.RoleName, "IX_UserToRoles_RoleName");

            entity.Property(e => e.UserId).HasMaxLength(256);
            entity.Property(e => e.RoleName).HasMaxLength(100);
            entity.Property(e => e.ConcurrencyToken)
                .IsRowVersion()
                .IsConcurrencyToken();

            entity.HasOne(d => d.RoleNameNavigation).WithMany(p => p.UserToRoles).HasForeignKey(d => d.RoleName);

            entity.HasOne(d => d.User).WithMany(p => p.UserToRoles).HasForeignKey(d => d.UserId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
