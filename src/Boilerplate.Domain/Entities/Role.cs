using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE ROLES DE LOS USUARIOS SPATIE
/// </summary>
public partial class Role
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string GuardName { get; set; }

    public string Routes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ModelHasRole> ModelHasRoles { get; } = new List<ModelHasRole>();

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
