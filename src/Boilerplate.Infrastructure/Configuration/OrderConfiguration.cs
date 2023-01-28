﻿using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel;
using System;

namespace Boilerplate.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.Property(e => e.Id).HasConversion<OrderId.EfCoreValueConverter>();
        entity.Property(e => e.ContactId).HasConversion<ContactId.EfCoreValueConverter>();
        entity.Property(e => e.UserId).HasConversion<UserId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Orders_Id_pkey");

        entity.ToTable("Orders", "web");

        entity.HasIndex(e => e.OrderNumber, "Orders_OrderNumber_key").IsUnique();

        entity.Property(e => e.Balance)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.CashAdvance)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.Credit).HasPrecision(14, 2);
        entity.Property(e => e.Dispatch).HasMaxLength(25);
        entity.Property(e => e.Documentation).HasColumnType("jsonb");
        entity.Property(e => e.Enterprise).HasMaxLength(50);
        entity.Property(e => e.ImgUrl).HasMaxLength(100);
        entity.Property(e => e.Iva)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.Notes).HasMaxLength(150);
        entity.Property(e => e.Observations).HasMaxLength(150);
        entity.Property(e => e.PaidState).HasDefaultValueSql("false");
        entity.Property(e => e.PaidUserType).HasMaxLength(1);
        entity.Property(e => e.PersonType).HasMaxLength(1);
        entity.Property(e => e.SubTotal)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.Total)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");

        entity.HasOne(d => d.Contact).WithMany(p => p.Orders)
            .HasForeignKey(d => d.ContactId)
            .HasConstraintName("Orders_ContactId_fk");

        /*entity.HasOne(d => d.User).WithMany(p => p.Orders)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("Orders_UserId_fk");*/
    }
}