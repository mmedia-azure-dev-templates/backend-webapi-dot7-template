using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Domain.Entities;
public class OrderItem : Entity<OrderItemId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override OrderItemId Id { get; set; }
    public ArticleId ArticleId { get; set; }
    public string DataKey { get; set; }
    public int? Quantity { get; set; }
    public decimal? Price { get; set; }
    public decimal? Total { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}