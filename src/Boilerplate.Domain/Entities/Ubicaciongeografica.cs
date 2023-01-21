using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA CON LA DISTRIBUCION GEOGRÁFICA DEL ECUADOR PROVINCIAS, CANTONES, PARROQUIAS
/// </summary>
public partial class Ubicaciongeografica
{
    public long Id { get; set; }

    public string Codigo { get; set; }

    public string Nombre { get; set; }

    public int? Idpadre { get; set; }

    public short Parroquia { get; set; }
}
