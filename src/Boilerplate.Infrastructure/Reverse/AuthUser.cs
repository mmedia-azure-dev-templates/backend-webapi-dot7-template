using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class AuthUser
{
    public string UserId { get; set; } = null!;

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public int? TenantId { get; set; }

    public byte[]? ConcurrencyToken { get; set; }

    public bool? IsDisabled { get; set; }

    public virtual Tenant? Tenant { get; set; }

    public virtual ICollection<UserToRole> UserToRoles { get; } = new List<UserToRole>();
}
