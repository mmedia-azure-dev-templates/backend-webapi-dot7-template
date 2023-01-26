using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class AuthPermissionsMigrationHistory
{
    public string MigrationId { get; set; } = null!;

    public string ProductVersion { get; set; } = null!;
}
