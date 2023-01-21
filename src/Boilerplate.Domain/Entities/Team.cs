using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA EQUIPOS
/// Columna name contiene el id del usuario
/// Columna parent_id contiene el id del padre
/// </summary>
public partial class Team
{
    public int Id { get; set; }

    public int Name { get; set; }

    public int Lft { get; set; }

    public int Rgt { get; set; }

    public int? ParentId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Team> InverseParent { get; } = new List<Team>();

    public virtual User NameNavigation { get; set; }

    public virtual Team Parent { get; set; }
}
