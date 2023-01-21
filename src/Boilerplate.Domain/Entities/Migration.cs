using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEFAULT DEL FRAMEWORK LARAVEL
/// </summary>
public partial class Migration
{
    public long Id { get; set; }

    public string Migration1 { get; set; }

    public int Batch { get; set; }
}
