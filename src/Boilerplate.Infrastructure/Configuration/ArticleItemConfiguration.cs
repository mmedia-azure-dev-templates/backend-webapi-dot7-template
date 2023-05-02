using Boilerplate.Domain.Entities;
using Boilerplate.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Boilerplate.Infrastructure.Configuration;

public class ArticleItemConfiguration : IEntityTypeConfiguration<ArticleItem>
{
    public void Configure(EntityTypeBuilder<ArticleItem> entity)
    {
        entity.HasKey(e => e.Id).HasName("PK_ArticlesItems");
        entity.Property(e => e.Id).HasConversion<ArticleItemId.EfCoreValueConverter>();
        entity.Property(e => e.Id).ValueGeneratedOnAdd();
        entity.Property(e => e.ArticleId).HasConversion<ArticleId.EfCoreValueConverter>();
        entity.Property(e => e.PaymentMethodId).HasConversion<PaymentMethodId.EfCoreValueConverter>();
    }
}