using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE PERMISOS DE LOS USUARIOS SPATIE 
/// </summary>
public partial class Permission
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string GuardName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<ModelHasPermission> ModelHasPermissions { get; } = new List<ModelHasPermission>();

    public virtual PermissionsCompany PermissionsCompany { get; set; }

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
