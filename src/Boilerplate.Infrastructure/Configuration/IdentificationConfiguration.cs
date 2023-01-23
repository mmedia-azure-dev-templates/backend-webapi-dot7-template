using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class IdentificationConfiguration : IEntityTypeConfiguration<Identification>
{
    public void Configure(EntityTypeBuilder<Identification> builder)
    {
        builder.HasKey(e => e.Id).HasName("Identifications_Id");
        builder.Property(e => e.Id).HasConversion<IdentificationId.EfCoreValueConverter>();
        builder.Property(e => e.UserId).HasConversion<UserId.EfCoreValueConverter>();
        builder.ToTable("Identifications", tb => tb.HasComment("TABLA HACE JOIN CON TABLA USERS AQUI SE ALMACENA LOS DATOS INFORMATIVOS DEL USUARIO"));
        builder.HasIndex(e => e.Ndocument, "Identifications_Ndocument_key").IsUnique();
        builder.HasIndex(e => e.UserId, "Identifications_UserId_key").IsUnique();
        builder.Property(e => e.Id).HasColumnName("Id");
        builder.Property(e => e.CatCivilStatus).HasColumnName("CatCivilStatus");
        builder.Property(e => e.CatGender).HasColumnName("CatGender");
        builder.Property(e => e.CatNacionality).HasColumnName("CatNacionality");
        builder.Property(e => e.CatTypeDocument).HasColumnName("CatTypeDocument");
        builder.Property(e => e.Mobile)
            .HasMaxLength(50)
            .HasColumnName("Mobile");
        builder.Property(e => e.Hired)
            .HasColumnName("Hired");
        builder.Property(e => e.Address)
            .HasMaxLength(200)
            .HasColumnName("Address");
        builder.Property(e => e.EntryDate).HasColumnName("EntryDate");
        builder.Property(e => e.BirthDate).HasColumnName("BirthDate");
        builder.Property(e => e.DepartureDate).HasColumnName("DepartureDate");
        builder.Property(e => e.CurriculumUrl)
            .HasMaxLength(200)
            .HasColumnName("CurriculumUrl");
        builder.Property(e => e.ImgUrl)
            .HasMaxLength(200)
            .HasColumnName("ImgUrl");
        builder.Property(e => e.Ndocument)
            .IsRequired()
            .HasMaxLength(15)
            .HasDefaultValueSql("'0'::bpchar")
            .HasColumnName("Ndocument");
        builder.Property(e => e.Notes)
            .HasMaxLength(50)
            .HasColumnName("Notes");
        builder.Property(e => e.Phone)
            .HasMaxLength(50)
            .HasColumnName("Phone");
        builder.Property(e => e.UbcCanton).HasColumnName("UbcCanton");
        builder.Property(e => e.UbcParroquia).HasColumnName("UbcParroquia");
        builder.Property(e => e.UbcProvincia).HasColumnName("UbcProvincia");
        builder.Property(e => e.UserId).HasColumnName("UserId");
    }
}