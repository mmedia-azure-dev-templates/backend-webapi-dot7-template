using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Tenant
{
    public int TenantId { get; set; }

    public string? ParentDataKey { get; set; }

    public string TenantFullName { get; set; } = null!;

    public bool IsHierarchical { get; set; }

    public int? ParentTenantId { get; set; }

    public byte[]? ConcurrencyToken { get; set; }

    public string? DatabaseInfoName { get; set; }

    public bool? HasOwnDb { get; set; }

    public virtual ICollection<AuthUser> AuthUsers { get; } = new List<AuthUser>();

    public virtual ICollection<Tenant> InverseParentTenant { get; } = new List<Tenant>();

    public virtual Tenant? ParentTenant { get; set; }

    public virtual ICollection<RoleToPermissionsTenant> RoleToPermissionsTenants { get; } = new List<RoleToPermissionsTenant>();
}
