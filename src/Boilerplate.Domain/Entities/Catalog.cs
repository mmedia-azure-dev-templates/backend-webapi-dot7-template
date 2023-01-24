using Boilerplate.Domain.Entities.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES
/// </summary>
public partial class Catalog : Entity<CatalogId>
{
    [Required]
    public override CatalogId Id { get; set; }

    public string Name { get; set; } = null!;

    public int? Parent { get; set; }

    public string? Value { get; set; }

    public virtual ICollection<Catalog> InverseParent { get; } = new List<Catalog>();
}
