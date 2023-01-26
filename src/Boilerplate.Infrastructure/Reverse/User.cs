using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? SurName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public string? Role { get; set; }

    public string? RememberToken { get; set; }

    public string? Email { get; set; }

    public int? IsActive { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? LastLoginIp { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}
