using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entity)
    {
        entity.Property(x => x.Id).HasConversion<UserId.EfCoreValueConverter>();

        entity.HasKey(e => e.Id).HasName("Users_Id_pkey");

        entity.ToTable("Users", "web", tb => tb.HasComment("EN ESTA TABLA SE GUARDAN LOS USUARIOS DEL SISTEMA"));

        entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

        entity.HasIndex(e => e.Name, "Users_Name_idx");

        entity.HasIndex(e => e.SurName, "Users_SurName_idx");

        entity.HasIndex(e => e.UserName, "Users_UserName_key").IsUnique();

        entity.Property(e => e.CreatedAt).HasColumnType("timestamp(0) without time zone");
        entity.Property(e => e.DeletedAt).HasColumnType("timestamp without time zone");
        entity.Property(e => e.Email).HasMaxLength(60);
        entity.Property(e => e.LastLogin).HasColumnType("timestamp without time zone");
        entity.Property(e => e.LastLoginIp)
            .HasMaxLength(50)
            .HasDefaultValueSql("'NULL::character varying'::character varying");
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.RememberToken)
            .HasMaxLength(100)
            .HasDefaultValueSql("'NULL::character varying'::character varying");
        entity.Property(e => e.SurName).HasMaxLength(50);
        entity.Property(e => e.UpdatedAt).HasColumnType("timestamp without time zone");
        entity.Property(e => e.UserName).HasMaxLength(50);
    }
}