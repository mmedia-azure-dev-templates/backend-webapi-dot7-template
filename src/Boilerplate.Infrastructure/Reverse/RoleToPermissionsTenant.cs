using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class RoleToPermissionsTenant
{
    public string TenantRolesRoleName { get; set; } = null!;

    public int TenantsTenantId { get; set; }

    public byte[]? ConcurrencyToken { get; set; }

    public virtual RoleToPermission TenantRolesRoleNameNavigation { get; set; } = null!;

    public virtual Tenant TenantsTenant { get; set; } = null!;
}
