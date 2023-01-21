using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class PermissionsCompany
{
    public int Id { get; set; }

    public string Company { get; set; }

    public int Permission { get; set; }

    public virtual Permission PermissionNavigation { get; set; }
}
