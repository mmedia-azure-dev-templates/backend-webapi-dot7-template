using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class CatalogConfiguration : IEntityTypeConfiguration<Catalog>
{
    public void Configure(EntityTypeBuilder<Catalog> entity)
    {
        entity.Property(e => e.Id).HasConversion<CatalogId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Catalogs_Id_pkey");
        entity.ToTable("Catalogs", "web", tb => tb.HasComment("TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES"));
        entity.Property(e => e.Name).HasMaxLength(150);
        entity.Property(e => e.Value).HasColumnType("jsonb");
    }
}