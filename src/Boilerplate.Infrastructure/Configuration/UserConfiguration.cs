﻿using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).HasConversion<UserId.EfCoreValueConverter>();

        builder.Property(e => e.SurName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("SurName");
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp(0) without time zone")
            .HasColumnName("CreatedAt");
        builder.Property(e => e.DeletedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("DeletedAt");
        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(254)
            .HasColumnName("Email");
        builder.Property(e => e.IsActive)
            .HasColumnName("IsActive");
        builder.Property(e => e.LastLogin)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("LastLogin");
        builder.Property(e => e.LastLoginIp)
            .HasMaxLength(50)
            .HasDefaultValueSql("NULL::character varying")
            .HasColumnName("LastLoginIp");
        builder.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("Name");
        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName("Password");
        builder.Property(e => e.RememberToken)
            .HasMaxLength(100)
            .HasDefaultValueSql("NULL::character varying")
            .HasColumnName("RememberToken");
        builder.Property(e => e.UpdatedAt)
            .HasColumnType("timestamp without time zone")
            .HasColumnName("UpdatedAt");
        builder.Property(e => e.UserName)
            .IsRequired()
            .HasMaxLength(50)
            .HasColumnName("UserName");
        
        builder.HasKey(e => e.Id).HasName("Users_Id");

        builder.HasIndex(x => x.Email).IsUnique();
        
        builder.HasIndex(e => e.SurName, "Idx_Users_SurName");

        builder.HasIndex(e => e.Name, "Idx_Users_Name");

        builder.HasIndex(e => e.Email, "Users_Email").IsUnique();

        builder.HasIndex(e => e.UserName, "Users_UserName").IsUnique();
    }
}