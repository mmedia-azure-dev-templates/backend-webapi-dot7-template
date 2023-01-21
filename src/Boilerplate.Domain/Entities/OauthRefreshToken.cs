using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEL FRAMEWORK LARAVEL OAUTH2
/// </summary>
public partial class OauthRefreshToken
{
    public string Id { get; set; }

    public string AccessTokenId { get; set; }

    public bool Revoked { get; set; }

    public DateTime? ExpiresAt { get; set; }
}
