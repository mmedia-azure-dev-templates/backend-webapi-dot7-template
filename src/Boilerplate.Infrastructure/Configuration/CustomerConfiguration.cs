using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Boilerplate.Infrastructure.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(e => e.Id).HasConversion<CustomerId.EfCoreValueConverter>();
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.HasIndex(e => e.Ndocument, "Customer_Ndocument_key").IsUnique();
        entity.Property(e => e.CivilStatusType).HasColumnName("CivilStatusType").HasConversion(
            v => v.ToString(),
            v => (CivilStatusType)Enum.Parse(typeof(CivilStatusType), v));
        entity.Property(e => e.GenderType).HasColumnName("GenderType").HasConversion(
            v => v.ToString(),
            v => (GenderType)Enum.Parse(typeof(GenderType), v));
        entity.Property(e => e.DocumentType).HasColumnName("DocumentType").HasConversion(
            v => v.ToString(),
            v => (IdentificationType)Enum.Parse(typeof(IdentificationType), v));
        entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
        entity.Property(e => e.Email).HasMaxLength(50);
        entity.Property(e => e.Mobile).HasMaxLength(20);
        entity.Property(e => e.Ndocument).HasMaxLength(15);
    }
}