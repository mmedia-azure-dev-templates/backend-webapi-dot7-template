using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class Inscription
{
    public int Insid { get; set; }

    public string Insconvenio { get; set; }

    public DateTime Insfecha { get; set; }

    public int Inssolicitante { get; set; }

    public string Insdatos { get; set; }
}
