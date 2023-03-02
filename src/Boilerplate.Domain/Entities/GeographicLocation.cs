using Boilerplate.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA CON LA DISTRIBUCION GEOGR�FICA DEL ECUADOR PROVINCIAS, CANTONES, PARROQUIAS
/// </summary>
[Table("GeographicLocations", Schema = "web")]
public class GeographicLocation : Entity<GeographicLocationId>
{
    [Required]
    public override GeographicLocationId Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? Parent { get; set; }

    public int Parroquia { get; set; }

    //public virtual ICollection<GeographicLocation> InverseParent { get; } = new List<GeographicLocation>();

    //public virtual GeographicLocation? Parent { get; set; }
}
