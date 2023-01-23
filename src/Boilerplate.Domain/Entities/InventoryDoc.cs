using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES
/// </summary>
public partial class InventoryDoc
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Description { get; set; } = null!;
}
