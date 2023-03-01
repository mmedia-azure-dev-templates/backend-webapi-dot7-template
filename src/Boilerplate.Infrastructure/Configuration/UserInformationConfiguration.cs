using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Reflection.Emit;

namespace Boilerplate.Infrastructure.Configuration;

public class UserInformationConfiguration : IEntityTypeConfiguration<UserInformation>
{
    public void Configure(EntityTypeBuilder<UserInformation> entity)
    {
        entity.HasKey(e => e.Id).HasName("UserInformations_Id");
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Id).HasConversion<IdentificationId.EfCoreValueConverter>();
        entity.Property(e => e.UserId).HasConversion<UserId.EfCoreValueConverter>();
        entity.HasIndex(e => e.Ndocument, "UserInformations_Ndocument_key").IsUnique();
        entity.HasIndex(e => e.UserId, "UserInformations_UserId_key").IsUnique();
        entity.Property(e => e.Id).HasColumnName("Id");
        entity.Property(e => e.CivilStatus).HasColumnName("CivilStatus").HasConversion(
            v => v.ToString(),
            v => (CivilStatusType)Enum.Parse(typeof(CivilStatusType), v));
        entity.Property(e => e.Gender).HasColumnName("Gender").HasConversion(
            v => v.ToString(),
            v => (GenderType)Enum.Parse(typeof(GenderType), v));
        entity.Property(e => e.Nacionality).HasColumnName("Nacionality").HasConversion(
            v => v.ToString(),
            v => (NacionalityType)Enum.Parse(typeof(NacionalityType), v));
        entity.Property(e => e.TypeDocument).HasColumnName("TypeDocument").HasConversion(
            v => v.ToString(),
            v => (IdentificationType)Enum.Parse(typeof(IdentificationType), v));
        entity.Property(e => e.Mobile)
            .HasMaxLength(50)
            .HasColumnName("Mobile");
        entity.Property(e => e.Hired)
            .HasColumnName("Hired");
        entity.Property(e => e.PrimaryStreet)
            .HasMaxLength(450)
            .HasColumnName("PrimaryStreet");
        entity.Property(e => e.SecondaryStreet)
            .HasMaxLength(450)
            .HasColumnName("SecondaryStreet");
        entity.Property(e => e.Numeration)
            .HasMaxLength(450)
            .HasColumnName("Numeration");
        entity.Property(e => e.Reference)
            .HasMaxLength(450)
            .HasColumnName("Reference");
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
        entity.Property(e => e.Canton).HasColumnName("Canton");
        entity.Property(e => e.Parroquia).HasColumnName("Parroquia");
        entity.Property(e => e.Provincia).HasColumnName("Provincia");
        entity.Property(e => e.UserId).HasColumnName("UserId");
    }
}