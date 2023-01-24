using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENAN LOS CLIENTES
/// </summary>
public partial class Contact : Entity<ContactId>
{
    [Required]
    public override ContactId Id { get; set; }

    public string? Ndocument { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }

    public string? Email { get; set; }

    public string? Mobile { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public int? CatTypeDocument { get; set; }

    public int? CatNacionality { get; set; }

    public int? UbcProvincia { get; set; }

    public int? UbcCanton { get; set; }

    public int? UbcParroquia { get; set; }

    public string? Notes { get; set; }

    public int? Supervisor { get; set; }

    public string? CatCivilStatus { get; set; }

    public DateTime? BirthDate { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
