using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES
/// </summary>
public partial class InventoryDoc : Entity<InventoryDocId>
{
    [Required]
    public override InventoryDocId Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;
}
