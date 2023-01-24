using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class InscriptionConfiguration : IEntityTypeConfiguration<Inscription>
{
    public void Configure(EntityTypeBuilder<Inscription> entity)
    {
        entity.Property(e => e.Id).HasConversion<InscriptionId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("Inscriptions_Id_pkey");

        entity.ToTable("Inscriptions", "web");

        entity.HasIndex(e => e.Applicant, "Inscriptions_Applicant_key").IsUnique();

        entity.Property(e => e.Agreement).HasMaxLength(50);
        entity.Property(e => e.Information).HasColumnType("jsonb");
    }
}