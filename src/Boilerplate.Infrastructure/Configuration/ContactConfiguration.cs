using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> entity)
    {
        entity.Property(e => e.Id).HasConversion<ContactId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Contacts_Id_pkey");

        entity.ToTable("Contacts", "web", tb => tb.HasComment("TABLA DONDE SE ALMACENAN LOS CLIENTES"));

        entity.HasIndex(e => e.Name, "Contacts_Name_idx").HasOperators(new[] { "varchar_pattern_ops" });

        entity.HasIndex(e => e.Ndocument, "Contacts_Ndocument_key").IsUnique();

        entity.HasIndex(e => e.SurName, "Contacts_SurName_idx").HasOperators(new[] { "varchar_pattern_ops" });

        entity.Property(e => e.Address).HasMaxLength(400);
        entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
        entity.Property(e => e.CatCivilStatus).HasMaxLength(25);
        entity.Property(e => e.Email).HasMaxLength(50);
        entity.Property(e => e.Mobile).HasMaxLength(20);
        entity.Property(e => e.Name).HasMaxLength(50);
        entity.Property(e => e.Ndocument).HasMaxLength(15);
        entity.Property(e => e.Notes).HasMaxLength(50);
        entity.Property(e => e.Phone).HasMaxLength(20);
        entity.Property(e => e.SurName).HasMaxLength(50);
    }
}