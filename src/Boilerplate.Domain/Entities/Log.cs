using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// SE ENCUENTRA INACTIVA 18-02-2022
/// </summary>
public partial class Log
{
    public long Id { get; set; }

    public string Usuario { get; set; }

    public string Operacion { get; set; }

    public string Usuarioafectado { get; set; }

    public string Valoranterior { get; set; }

    public string Valoractual { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
