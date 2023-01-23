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
    public string Name { get; set; } = string.Empty;
    public string SurName { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string RememberToken { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public short IsActive { get; set; }
    public DateTime? LastLogin { get; set; }
    public string LastLoginIp { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
}