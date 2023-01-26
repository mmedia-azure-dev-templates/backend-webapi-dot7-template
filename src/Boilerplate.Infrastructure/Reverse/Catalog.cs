using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class Catalog
{
    public int? Id { get; set; }

    public string? Name { get; set; }

    public int? CustomParent { get; set; }

    public string? CustomData { get; set; }
}
