using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE LOS PRODUCTOS ASOCIADO A UNA ORDEN
/// </summary>
public partial class Orderproduct
{
    public int Id { get; set; }

    public int? Ordernumero { get; set; }

    public string Sku { get; set; }

    public string Brand { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal? Weight { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }
}
