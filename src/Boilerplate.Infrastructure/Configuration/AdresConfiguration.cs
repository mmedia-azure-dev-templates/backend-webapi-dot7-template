using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class AdresConfiguration : IEntityTypeConfiguration<Addres>
{
    public void Configure(EntityTypeBuilder<Addres> entity)
    {
        entity.Property(e => e.Id).HasConversion<AddresId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("PK_Address");
        entity.Property(e => e.PersonId).HasConversion<PersonId.EfCoreValueConverter>();
    }
}