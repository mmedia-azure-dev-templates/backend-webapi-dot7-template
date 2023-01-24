using Boilerplate.Domain.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DE CONTADORES DE ORDENES DEL SISTEMA
/// </summary>
public partial class Counter : Entity<CounterId>
{
    [Required]
    public override CounterId Id { get; set; }

    public string Slug { get; set; } = null!;

    public long CustomCounter { get; set; }
}
