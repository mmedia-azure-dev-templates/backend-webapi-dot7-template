using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class InventoryDocConfiguration : IEntityTypeConfiguration<InventoryDoc>
{
    public void Configure(EntityTypeBuilder<InventoryDoc> entity)
    {
        entity.Property(e => e.Id).HasConversion<InventoryDocId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("InventoryDocs_Id_pkey");

        entity.ToTable("InventoryDocs", "web", tb => tb.HasComment("TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES"));

        entity.HasIndex(e => e.Code, "InventoryDocs_Code_key").IsUnique();

        entity.HasIndex(e => e.Description, "InventoryDocs_Description_key").IsUnique();
    }
}