using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
    public void Configure(EntityTypeBuilder<Article> entity)
    {
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.Id).HasConversion<ArticleId.EfCoreValueConverter>();
        entity.HasKey(e => e.Id).HasName("PK_Articles");
        entity.ToTable("Articles", "web");
        entity.Property(e => e.Meta).HasColumnType("nvarchar(max)");
        entity.Property(e => e.Display).HasColumnType("nvarchar(max)");
        entity.Property(e => e.Notes).HasMaxLength(150);
        entity.Property(e => e.Sku).HasMaxLength(10);
    }
}