using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection.Emit;

namespace Boilerplate.Infrastructure.Configuration;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> entity)
    {
        entity.Property(e => e.Id).HasConversion<PaymentId.EfCoreValueConverter>();
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.OrderId).HasConversion<OrderId.EfCoreValueConverter>();
        entity.Property(e => e.PaymentMethodId).HasConversion<PaymentMethodId.EfCoreValueConverter>();
    }
}