using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE CONTADORES DE ORDENES DEL SISTEMA
/// </summary>
public partial class Counter
{
    public int Id { get; set; }

    public string Slug { get; set; } = null!;

    public long CustomCounter { get; set; }
}
