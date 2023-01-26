using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class RoleToPermission
{
    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public string PackedPermissionsInRole { get; set; } = null!;

    public byte[]? ConcurrencyToken { get; set; }

    public byte RoleType { get; set; }

    public virtual ICollection<RoleToPermissionsTenant> RoleToPermissionsTenants { get; } = new List<RoleToPermissionsTenant>();

    public virtual ICollection<UserToRole> UserToRoles { get; } = new List<UserToRole>();
}
