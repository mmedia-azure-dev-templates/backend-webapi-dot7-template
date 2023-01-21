using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class Articulo
{
    public int Artid { get; set; }

    public int? Artproveedor { get; set; }

    public string Artsku { get; set; }

    public string Artabrevia { get; set; }

    public string Artnombre { get; set; }

    public decimal Artcosto { get; set; }

    public int? Artmarca { get; set; }

    public string Artnotas { get; set; }

    public string Artmeta { get; set; }

    public bool? Artdiscontinued { get; set; }
}
