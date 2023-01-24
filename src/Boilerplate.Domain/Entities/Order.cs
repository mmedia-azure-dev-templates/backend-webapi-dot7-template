using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public partial class Order : Entity<OrderId>
{
    [Required]
    public override OrderId Id { get; set; }

    public string Enterprise { get; set; } = null!;

    public DateOnly GeneratedDate1 { get; set; }

    public DateOnly? GeneratedDate2 { get; set; }

    public TimeOnly GeneratedHour1 { get; set; }

    public int OrderNumber { get; set; }

    public decimal? Credit { get; set; }
    
    public UserId UserId { get; set; }

    public ContactId ContactId { get; set; }

    public int? Assigned { get; set; }

    public char? PersonType { get; set; }

    public decimal CashAdvance { get; set; }

    public decimal SubTotal { get; set; }

    public decimal Iva { get; set; }

    public decimal Total { get; set; }

    public decimal Balance { get; set; }

    public int? Agreegment { get; set; }

    public int? Term { get; set; }

    public int? State { get; set; }

    public string? Observations { get; set; }

    public string? Notes { get; set; }

    public string? ImgUrl { get; set; }

    public string? Documentation { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? PaidUser { get; set; }

    public char? PaidUserType { get; set; }

    public bool? PaidState { get; set; }

    public string? Dispatch { get; set; }

    public string? Extras { get; set; }

    public virtual Contact? Contact { get; set; }

    public virtual User User { get; set; } = null!;
}
