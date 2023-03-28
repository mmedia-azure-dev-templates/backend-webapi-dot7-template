using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Boilerplate.Infrastructure.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> entity)
    {
        entity.Property(e => e.Id).HasConversion<OrderId.EfCoreValueConverter>();
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.OrderStatusType).HasColumnName("OrderStatusType").HasConversion(
            v => v.ToString(),
            v => (OrderStatusType)Enum.Parse(typeof(OrderStatusType), v));
        entity.Property(e => e.CustomerId).HasConversion<CustomerId.EfCoreValueConverter>();
        entity.Property(e => e.UserGenerated).HasConversion<UserGenerated.EfCoreValueConverter>();
        entity.Property(e => e.UserAssigned).HasConversion<UserAssigned.EfCoreValueConverter>();
        entity.Property(e => e.OrderNumber).HasConversion<OrderNumber.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("PK_Orders");
    }
}