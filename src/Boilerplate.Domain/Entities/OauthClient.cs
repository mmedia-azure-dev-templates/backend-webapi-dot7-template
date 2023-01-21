using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEL FRAMEWORK LARAVEL OAUTH2
/// </summary>
public partial class OauthClient
{
    public long Id { get; set; }

    public long? UserId { get; set; }

    public string Name { get; set; }

    public string Secret { get; set; }

    public string Provider { get; set; }

    public string Redirect { get; set; }

    public bool PersonalAccessClient { get; set; }

    public bool PasswordClient { get; set; }

    public bool Revoked { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}
