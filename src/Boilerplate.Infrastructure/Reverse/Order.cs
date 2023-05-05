using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Order
{
    public int? Id { get; set; }

    public string? Enterprise { get; set; }

    public DateTime? GeneratedDate1 { get; set; }

    public DateTime? GeneratedDate2 { get; set; }

    public DateTime? GeneratedHour1 { get; set; }

    public int? OrderNumber { get; set; }

    public decimal? Credit { get; set; }

    public int? UserId { get; set; }

    public int? ContactId { get; set; }

    public int? Assigned { get; set; }

    public string? PersonType { get; set; }

    public decimal? SubTotal { get; set; }

    public decimal? Iva { get; set; }

    public decimal? Total { get; set; }

    public decimal? Balance { get; set; }

    public int? Agreegment { get; set; }

    public int? Term { get; set; }

    public int? State { get; set; }

    public string? Observations { get; set; }

    public string? Notes { get; set; }

    public string? ImgUrl { get; set; }

    public string? Documentation { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? PaidUser { get; set; }

    public string? PaidUserType { get; set; }

    public bool? PaidState { get; set; }

    public string? Dispatch { get; set; }

    public string? Extras { get; set; }
}
