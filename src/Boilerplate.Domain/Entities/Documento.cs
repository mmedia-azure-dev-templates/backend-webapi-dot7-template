using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DONDE SE ALMACENA EL INVENTARIO DE LOS DOCUMENTOS REQUERIDOS EN LAS ORDENES
/// </summary>
public partial class Documento
{
    public int Docuid { get; set; }

    public string Docucodigo { get; set; }

    public string Docudescripcion { get; set; }
}
