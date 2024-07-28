using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Tranchy.Common;

public class Tenant(IHttpContextAccessor httpContextAccessor) : ITenant
{
    private IEnumerable<Claim> Claims => httpContextAccessor?.HttpContext?.User?.Claims ?? [];

    public string Email => GetClaim(ClaimTypes.Email);

    public string UserId => GetClaim(ClaimTypes.NameIdentifier);

    private string GetClaim(string claimType)
    {
        var claim = Claims.FirstOrDefault(c => string.Equals(c.Type, claimType, StringComparison.Ordinal));
        return claim?.Value ?? throw new ArgumentNullException(claimType);
    }
}