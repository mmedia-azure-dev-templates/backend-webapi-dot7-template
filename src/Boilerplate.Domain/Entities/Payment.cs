using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Implementations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public class Payment : Entity<PaymentId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override PaymentId Id { get; set; }
    public string DataKey { get; set; }
    public OrderId OrderId { get; set; }
    public PaymentMethodId? PaymentMethodId { get; set; }
    public decimal? Amount { get; set; }
    public string? Notes { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
