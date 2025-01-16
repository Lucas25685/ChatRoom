using Chat.Api.Infrastructure.Authorization.Policies;

namespace Chat.Api.Infrastructure.Authorization;

/// <summary>
/// Provides extension methods for authorization.
/// </summary>
public static class AuthorizationExtensions
{
    /// <summary>
    /// Adds authorization policies for the platform.
    /// </summary>
    public static void AddApplicationAuthorization(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorizationBuilder()
            .AddPolicy(AuthorizationPolicies.AccountEnabled, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.Requirements.Add(new AccountEnabledRequirement());
            });
    }
}