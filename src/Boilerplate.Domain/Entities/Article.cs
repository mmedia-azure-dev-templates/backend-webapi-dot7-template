using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class Article
{
    public int Id { get; set; }

    public int? Provider { get; set; }

    public string? Sku { get; set; }

    public string? Abrevia { get; set; }

    public string? Name { get; set; }

    public decimal Cost { get; set; }

    public int? Brand { get; set; }

    public string? Notes { get; set; }

    public string? Meta { get; set; }

    public bool? Discontinued { get; set; }
}
