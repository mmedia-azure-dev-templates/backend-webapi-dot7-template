using AuthPermissions.BaseCode.CommonCode;
using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using Boilerplate.Domain.Implementations;
using Microsoft.Graph.ExternalConnectors;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Orders", Schema = "web")]
public class Order : Entity<OrderId>, IDataKeyFilterReadWrite, IDateCreatedAndUpdated
{
    public override OrderId Id { get; set; }
    public string DataKey { get; set; }
    public PaymentMethodsType AgreegmentPaymentType { get; set; }
    public OrderStatusType OrderStatusType { get; set; }
    public OrderNumber OrderNumber { get; set; }
    public decimal? Credit { get; set; }
    public UserGenerated UserGenerated { get; set; }
    public UserAssigned UserAssigned { get; set; }
    public CustomerId CustomerId { get; set; }
    public decimal CashAdvance { get; set; }
    public decimal SubTotal { get; set; }
    public decimal Iva { get; set; }
    public decimal Total { get; set; }
    public decimal Balance { get; set; }
    public int? Term { get; set; }
    public string? Observations { get; set; }
    public string? Notes { get; set; }
    public string? DocumentUrl { get; set; }
    public string? Documentation { get; set; }
    public DateTime? PaidDate { get; set; }
    public UserPaid? UserPaid { get; set; }
    public bool? PaidState { get; set; }
    public string? Dispatch { get; set; }
    public string? Extras { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    //public virtual Contact? Contact { get; set; }
    //public virtual User User { get; set; } = null!;
}
