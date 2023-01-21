using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

public partial class Job
{
    public long Id { get; set; }

    public string Queue { get; set; }

    public string Payload { get; set; }

    public short Attempts { get; set; }

    public int? ReservedAt { get; set; }

    public int AvailableAt { get; set; }

    public int CreatedAt { get; set; }
}
