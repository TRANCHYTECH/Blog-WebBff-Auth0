using System.Security.Claims;

namespace Tranchy.Common;

public static class ClaimExtensions
{
    public static string UserName(this ClaimsPrincipal principal)
    {
        var email = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email);

        return email?.Value ?? throw new ArgumentException("principal");
    }
}