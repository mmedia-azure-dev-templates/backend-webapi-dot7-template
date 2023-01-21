using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE CONTADORES DE ORDENES DEL SISTEMA
/// </summary>
public partial class Contadore
{
    public int Id { get; set; }

    public string Slug { get; set; }

    public long Customcounter { get; set; }
}
