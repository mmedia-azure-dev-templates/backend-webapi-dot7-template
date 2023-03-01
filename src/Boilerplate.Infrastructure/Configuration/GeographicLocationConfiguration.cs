using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class GeographicLocationConfiguration : IEntityTypeConfiguration<GeographicLocation>
{
    public void Configure(EntityTypeBuilder<GeographicLocation> entity)
    {
        entity.Property(e => e.Id).HasConversion<GeographicLocationId.EfCoreValueConverter>();
    }
}