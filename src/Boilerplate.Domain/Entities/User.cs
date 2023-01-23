using Boilerplate.Domain.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boilerplate.Domain.Entities;

[Table("Users", Schema = "web")]
public class User : Entity<UserId>
{
    [Required]
    public override UserId Id { get; set; }
    public string Name { get; set; } = null!;

    public string SurName { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string? RememberToken { get; set; }

    public string Email { get; set; } = null!;

    public short IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? LastLoginIp { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual Identification? Identification { get; set; }
}