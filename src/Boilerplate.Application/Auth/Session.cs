using Boilerplate.Domain.Entities.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;
using ISession = Boilerplate.Domain.Implementations.ISession;

namespace Boilerplate.Application.Auth;

public class Session : ISession
{
    public UserId UserId { get; private init; }
    public string TenantName { get; private init; }
    public string DataKey { get; private init;}
    public ClaimsPrincipal? User { get; private init; }
    public DateTime Now => DateTime.Now;

    public Session(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        var tenantName = user.FindFirst("TenantName");
        var dataKey = user.FindFirst("DataKey");
        var nameIdentifier = user?.FindFirst(ClaimTypes.NameIdentifier);

        User = user;

        if(nameIdentifier != null)
        {
            UserId = new UserId(new Guid(nameIdentifier.Value));
        }

        if(tenantName != null)
        {
            TenantName = tenantName.Value;
        }

        if(dataKey != null)
        {
            DataKey = dataKey.Value;
        }
    }
}