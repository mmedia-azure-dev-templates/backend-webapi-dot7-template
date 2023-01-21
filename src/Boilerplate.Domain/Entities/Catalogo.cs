using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA MAESTRA CATALOGO DEL SISTEMA CONTIENE DE TODO CONFIGURACIONES
/// </summary>
public partial class Catalogo
{
    public long Id { get; set; }

    public string Nombre { get; set; }

    public int? Idpadre { get; set; }

    public string Valor { get; set; }
}
