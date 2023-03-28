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
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.OrderId).HasConversion<OrderId.EfCoreValueConverter>();
        entity.Property(e => e.ArticleId).HasConversion<ArticleId.EfCoreValueConverter>();
    }
}