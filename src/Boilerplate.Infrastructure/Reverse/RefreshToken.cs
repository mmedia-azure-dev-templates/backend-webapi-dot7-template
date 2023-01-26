using System;
using System.Collections.Generic;

namespace Boilerplate.Infrastructure.Reverse;

public partial class RefreshToken
{
    public string TokenValue { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string? JwtId { get; set; }

    public bool IsInvalid { get; set; }

    public DateTime AddedDateUtc { get; set; }

    public byte[]? ConcurrencyToken { get; set; }
}
