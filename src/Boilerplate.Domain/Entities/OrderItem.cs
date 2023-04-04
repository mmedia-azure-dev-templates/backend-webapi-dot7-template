using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;
[Table("OrderItems", Schema = "web")]
public class OrderItem : Entity<OrderItemId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override OrderItemId Id { get; set; }
    public string DataKey { get; set; }
    public OrderId OrderId { get; set; }
    public ArticleId ArticleId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Total { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}