using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE PERMISOS DE LOS USUARIOS SPATIE 
/// </summary>
public partial class ModelHasRole
{
    public int RoleId { get; set; }

    public string ModelType { get; set; }

    public long ModelId { get; set; }

    public virtual Role Role { get; set; }
}
