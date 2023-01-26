using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class UserToRole
{
    public string UserId { get; set; } = null!;

    public string RoleName { get; set; } = null!;

    public byte[]? ConcurrencyToken { get; set; }

    public virtual RoleToPermission RoleNameNavigation { get; set; } = null!;

    public virtual AuthUser User { get; set; } = null!;
}
