using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class GeographicLocation
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public int? Parent { get; set; }

    public int? Parroquia { get; set; }
}
