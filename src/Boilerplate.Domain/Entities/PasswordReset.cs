using System;
using System.Collections.Generic;

namespace Boilerplate.Domain.Entities;

/// <summary>
/// TABLA DEL FRAMEWORK SE GUARDAN LOS RESETS PARA RECUPERAR PASSWORD
/// </summary>
public partial class PasswordReset
{
    public string Email { get; set; }

    public string Token { get; set; }

    public DateTime CreatedAt { get; set; }
}
