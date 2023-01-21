using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE GUARDAN LOS DEPOSITOS
/// </summary>
public partial class Payment
{
    public int Payid { get; set; }

    public DateTime? Paydate { get; set; }

    public int Payentidadfinanciera { get; set; }

    public int Payorden { get; set; }

    public decimal Payammount { get; set; }

    public int Payusugenera { get; set; }

    public string Payobservacion { get; set; }

    public string Paytransaction { get; set; }
}
