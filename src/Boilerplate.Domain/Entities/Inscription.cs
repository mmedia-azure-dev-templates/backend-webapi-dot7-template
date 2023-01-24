using Boilerplate.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

public partial class Inscription : Entity<InscriptionId>
{
    [Required]
    public override InscriptionId Id { get; set; }

    public string Agreement { get; set; } = null!;

    public DateTime InscriptionDate { get; set; }

    public int Applicant { get; set; }

    public string? Information { get; set; }
}
