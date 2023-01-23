using Boilerplate.Domain.Entities.Common;
using Boilerplate.Domain.Entities.Enums;
using MassTransit;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Heroes", Schema = "web")]
public class Hero : Entity<HeroId>
{
    public override HeroId Id { get; set; }
    public string Name { get; set; } = null!;

    public string? Nickname { get; set; }

    public string? Individuality { get; set; }

    public int? Age { get; set; }

    public HeroType HeroType { get; set; }

    public string? Team { get; set; }
}