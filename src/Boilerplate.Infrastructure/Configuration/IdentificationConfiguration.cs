using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Graph;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class IdentificationConfiguration : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> entity)
    {
        entity.HasKey(e => e.Id).HasName("Identifications_Id");
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Id).HasConversion<IdentificationId.EfCoreValueConverter>();
        entity.Property(e => e.UserId).HasConversion<UserId.EfCoreValueConverter>();
        entity.HasIndex(e => e.Ndocument, "Identifications_Ndocument_key").IsUnique();
        entity.HasIndex(e => e.UserId, "Identifications_UserId_key").IsUnique();
        entity.Property(e => e.Id).HasColumnName("Id");
        entity.Property(e => e.CatCivilStatus).HasColumnName("CatCivilStatus");
        entity.Property(e => e.CatGender).HasColumnName("CatGender");
        entity.Property(e => e.CatNacionality).HasColumnName("CatNacionality");
        entity.Property(e => e.CatTypeDocument).HasColumnName("CatTypeDocument");
        entity.Property(e => e.Mobile)
            .HasMaxLength(50)
            .HasColumnName("Mobile");
        entity.Property(e => e.Hired)
            .HasColumnName("Hired");
        entity.Property(e => e.Address)
            .HasMaxLength(200)
            .HasColumnName("Address");
        entity.Property(e => e.EntryDate).HasColumnName("EntryDate");
        entity.Property(e => e.BirthDate).HasColumnName("BirthDate");
        entity.Property(e => e.DepartureDate).HasColumnName("DepartureDate");
        entity.Property(e => e.CurriculumUrl)
            .HasMaxLength(200)
            .HasColumnName("CurriculumUrl");
        entity.Property(e => e.ImgUrl)
            .HasMaxLength(200)
            .HasColumnName("ImgUrl");
        entity.Property(e => e.Ndocument)
            .IsRequired()
            .HasMaxLength(15)
            .HasColumnName("Ndocument");
        entity.Property(e => e.Notes)
            .HasMaxLength(50)
            .HasColumnName("Notes");
        entity.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("Phone");
        entity.Property(e => e.UbcCanton).HasColumnName("UbcCanton");
        entity.Property(e => e.UbcParroquia).HasColumnName("UbcParroquia");
        entity.Property(e => e.UbcProvincia).HasColumnName("UbcProvincia");
        entity.Property(e => e.UserId).HasColumnName("UserId");
    }
}