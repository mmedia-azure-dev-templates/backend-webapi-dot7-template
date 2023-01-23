using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA CON LA DISTRIBUCION GEOGR�FICA DEL ECUADOR PROVINCIAS, CANTONES, PARROQUIAS
/// </summary>
public partial class GeographicLocation
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? ParentId { get; set; }

    public short Parroquia { get; set; }

    public virtual ICollection<GeographicLocation> InverseParent { get; } = new List<GeographicLocation>();

    public virtual GeographicLocation? Parent { get; set; }
}
