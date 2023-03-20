using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> entity)
    {
        entity.Property(e => e.Id).HasConversion<OrderItemId.EfCoreValueConverter>();
        entity.Property(e => e.ArticleId).HasConversion<ArticleId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Products_Id_pkey");
        entity.Property(e => e.Price).HasPrecision(14, 2);
        entity.Property(e => e.Total).HasPrecision(14, 2);
    }
}