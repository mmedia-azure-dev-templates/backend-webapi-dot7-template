using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> entity)
    {
        entity.Property(e => e.Id).HasConversion<CustomerId.EfCoreValueConverter>();
        //entity.HasKey(e => e.Id).HasName("Contacts_Id_pkey");
        entity.HasIndex(e => e.Ndocument, "Customer_Ndocument_key").IsUnique();
        entity.Property(e => e.BirthDate).HasColumnType("timestamp without time zone");
        entity.Property(e => e.Email).HasMaxLength(50);
        entity.Property(e => e.Mobile).HasMaxLength(20);
        entity.Property(e => e.Ndocument).HasMaxLength(15);
    }
}