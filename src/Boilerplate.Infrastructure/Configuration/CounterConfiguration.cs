using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class CounterConfiguration : IEntityTypeConfiguration<Counter>
{
    public void Configure(EntityTypeBuilder<Counter> entity)
    {
        entity.Property(e => e.Id).HasConversion<CounterId.EfCoreValueConverter>();
        entity.Property(e => e.CustomCounter).HasConversion<CustomCounter.EfCoreValueConverter>();
        entity.Property(e => e.Slug).HasMaxLength(50);
    }
}