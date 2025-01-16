using Chat.Api.Infrastructure.Authorization.Policies;

namespace Chat.Api.Infrastructure.Authorization;

/// <summary>
/// Contains the authorization policies for the platform.
/// </summary>
public static class AuthorizationPolicies
{
    /// <summary>
    /// Requires the current user's account to be enabled.
    /// </summary>
    /// <seealso cref="AccountEnabledRequirement"/>
    public const string AccountEnabled = "AccountEnabled";

}