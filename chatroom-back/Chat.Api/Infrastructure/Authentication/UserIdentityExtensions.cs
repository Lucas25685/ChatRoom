using System.Security.Claims;

namespace Chat.Api.Infrastructure.Authentication;

/// <summary>
/// Contains extension methods for user identity.
/// </summary>
public static class UserIdentityExtensions
{
    /// <summary>
    /// Gets the user email from the specified <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The user email, or null if not found.</returns>
    public static string? GetUserEmail(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.Email);
    }
    
    /// <summary>
    /// Gets the name identifier from the specified <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The user email, or null if not found.</returns>
    public static string? GetNameIdentifier(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
    
    /// <summary>
    /// Gets the user OIDC ID from the specified <see cref="ClaimsPrincipal"/>.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <returns>The user OIDC ID, or null if not found.</returns>
    public static string? GetUserUid(this ClaimsPrincipal principal)
    {
        return principal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}