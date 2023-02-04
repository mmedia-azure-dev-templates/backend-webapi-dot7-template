using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BC = BCrypt.Net.BCrypt;

namespace Boilerplate.Infrastructure.Configuration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> entity)
    {
        //entity.Property(e => e.LegacyId).HasConversion<UserId.EfCoreValueConverter>();
        entity.HasKey(x => x.LegacyId);
        entity.Property(e => e.LegacyId).ValueGeneratedOnAdd();
    }
}