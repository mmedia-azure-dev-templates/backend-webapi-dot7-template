using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// SE ALMACENA LAS SESIONES DEL USUARIOS ESTA TABLA ES DEL FRAMEWORK
/// </summary>
public partial class Session
{
    public string Id { get; set; }

    public int? UserId { get; set; }

    public string IpAddress { get; set; }

    public string UserAgent { get; set; }

    public string Payload { get; set; }

    public int LastActivity { get; set; }
}
