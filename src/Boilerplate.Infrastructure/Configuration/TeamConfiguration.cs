using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class TeamConfiguration : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> entity)
    {
        entity.Property(e => e.Id).HasConversion<TeamId.EfCoreValueConverter>();
        entity.Property(e => e.UserId).HasConversion<UserId.EfCoreValueConverter>();
    }
}