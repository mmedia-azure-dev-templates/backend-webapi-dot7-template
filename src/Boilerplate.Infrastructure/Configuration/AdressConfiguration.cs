using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class AdressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> entity)
    {
        entity.Property(e => e.Id).HasConversion<AddressId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("PK_Address");
        entity.Property(e => e.PersonId).HasConversion<PersonId.EfCoreValueConverter>();
    }
}