﻿using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Orders", Schema = "web")]
public class Order : Entity<OrderId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override OrderId Id { get; set; }
    public string DataKey { get; set; } = null!;
    public bool Locked { get; set; } = false;
    public OrderStatusType OrderStatusType { get; set; }
    public OrderNumber OrderNumber { get; set; }
    public PaymentMethodId? PaymentMethodId { get; set; } = null!;
    public UserGenerated UserGenerated { get; set; }
    public UserAssigned? UserAssigned { get; set; } = null!;
    public CustomerId? CustomerId { get; set; } = null;
    public decimal SubTotal { get; set; } = 0;
    public decimal Total { get; set; } = 0;
    public string? Notes { get; set; } = null;
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
