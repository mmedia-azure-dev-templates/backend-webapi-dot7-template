using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Product
{
    public int? Id { get; set; }

    public int? OrderId { get; set; }

    public string? Sku { get; set; }

    public string? Brand { get; set; }

    public string? Name { get; set; }

    public string? Descriptions { get; set; }

    public decimal? Weigth { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }
}
