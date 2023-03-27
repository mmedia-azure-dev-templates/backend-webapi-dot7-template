using Boilerplate.Domain.Entities;
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
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.CustomerId).HasConversion<CustomerId.EfCoreValueConverter>();
        entity.Property(e => e.UserGenerated).HasConversion<UserGenerated.EfCoreValueConverter>();
        entity.Property(e => e.UserAssigned).HasConversion<UserAssigned.EfCoreValueConverter>();
        entity.Property(e => e.OrderNumber).HasConversion<OrderNumber.EfCoreValueConverter>();
        entity.Property(e => e.UserPaid).HasConversion<UserPaid.EfCoreValueConverter>();

        entity.HasKey(e => e.Id).HasName("Orders_Id_pkey");

        entity.ToTable("Orders", "web");

        entity.HasIndex(e => e.OrderNumber, "Orders_OrderNumber_key").IsUnique();

        entity.Property(e => e.Dispatch).HasMaxLength(25);
        entity.Property(e => e.Documentation).HasColumnType("jsonb");
        entity.Property(e => e.DocumentUrl).HasMaxLength(100);
        entity.Property(e => e.Iva)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.Notes).HasMaxLength(150);
        entity.Property(e => e.Observations).HasMaxLength(150);
        entity.Property(e => e.PaidState).HasDefaultValueSql("false");
        entity.Property(e => e.SubTotal)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
        entity.Property(e => e.Total)
            .HasPrecision(14, 2)
            .HasDefaultValueSql("0.00");
    }
}