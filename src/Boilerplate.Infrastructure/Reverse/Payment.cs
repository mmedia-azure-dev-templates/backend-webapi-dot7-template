using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Payment
{
    public int? Id { get; set; }

    public DateTime? PaidDate { get; set; }

    public int? FinancialEntity { get; set; }

    public int? OrderNumber { get; set; }

    public decimal? Ammount { get; set; }

    public int? UsuId { get; set; }

    public string? Observation { get; set; }

    public string? Transaction { get; set; }
}
