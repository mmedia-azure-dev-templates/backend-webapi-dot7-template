using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("ArticlesItems", Schema = "web")]
public class ArticleItem : Entity<ArticleItemId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override ArticleItemId Id { get; set; }
    public string DataKey { get; set; } = null!;
    public ArticleId ArticleId { get; set; }
    public PaymentMethodId PaymentMethodId { get; set; }
    public decimal Price { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
