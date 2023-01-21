using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEL FRAMEWORK LARAVEL OAUTH2
/// </summary>
public partial class OauthPersonalAccessClient
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
