using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEL FRAMEWORK LARAVEL OAUTH2
/// </summary>
public partial class OauthAuthCode
{
    public string Id { get; set; }

    public long UserId { get; set; }

    public long ClientId { get; set; }

    public string Scopes { get; set; }

    public bool Revoked { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
