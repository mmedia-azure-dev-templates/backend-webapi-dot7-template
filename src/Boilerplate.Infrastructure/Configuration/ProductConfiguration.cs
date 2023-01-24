using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> entity)
    {
        entity.Property(e => e.Id).HasConversion<ProductId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Products_Id_pkey");

        entity.ToTable("Products", "web", tb => tb.HasComment("TABLA DE LOS PRODUCTOS ASOCIADO A UNA ORDEN"));

        entity.HasIndex(e => e.OrderId, "Products_OrderId_idx");

        entity.Property(e => e.Price).HasPrecision(14, 2);
        entity.Property(e => e.Total).HasPrecision(14, 2);
        entity.Property(e => e.Weigth).HasPrecision(14, 1);
    }
}