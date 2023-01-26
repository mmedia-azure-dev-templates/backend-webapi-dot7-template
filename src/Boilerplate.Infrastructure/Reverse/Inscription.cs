using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Inscription
{
    public int? Id { get; set; }

    public string? Agreement { get; set; }

    public DateTime? InscriptionDate { get; set; }

    public int? Applicant { get; set; }

    public string? Information { get; set; }
}
