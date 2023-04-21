using Boilerplate.Domain.Entities.Common;
using System;
using System.Security.Claims;

namespace Boilerplate.Domain.Implementations;

public interface ISession
{
    public UserId UserId { get; }
    public string TenantName { get; }
    public string DataKey { get; }
    public ClaimsPrincipal User { get; }
    public DateTime Now { get; }
}