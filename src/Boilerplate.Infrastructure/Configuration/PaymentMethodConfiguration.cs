using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Boilerplate.Infrastructure.Configuration;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Id).HasConversion<PaymentMethodId.EfCoreValueConverter>();
        entity.Property(e => e.PaymentMethodsType).HasColumnName("PaymentMethodsType").HasConversion(
            v => v.ToString(),
            v => (PaymentMethodsType)Enum.Parse(typeof(PaymentMethodsType), v));
    }
}