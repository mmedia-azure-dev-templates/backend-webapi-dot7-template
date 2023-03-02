using Boilerplate.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Boilerplate.Application.Features.GeographicLocation;
public class GeographicLocationResponse
{
    public GeographicLocationId Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int? Parent { get; set; }

    public int Parroquia { get; set; }
}
