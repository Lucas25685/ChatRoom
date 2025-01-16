using Microsoft.AspNetCore.Authorization;

namespace Chat.Api.Infrastructure.Authorization.Policies;

/// <summary>
/// Represents a requirement for an enabled account.
/// </summary>
public sealed class AccountEnabledRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AccountEnabledRequirement"/> class.
    /// </summary>
    public AccountEnabledRequirement() { }
}