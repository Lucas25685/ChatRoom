using Microsoft.AspNetCore.Identity;
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Chat.Model.Auth;

/// <summary>
/// Represents a platform role.
/// </summary>
public sealed class Role : IdentityRole<Guid>
{
    public static Role SuperAdmin { get; } = new()
    {
        Id = Guid.Parse("5d0659c3-811e-447e-9935-5404ab23e120"),
        Name = Roles.SuperAdmin,
        NormalizedName = Roles.SuperAdmin.ToUpperInvariant()
    };

    public static Role CompanyAdmin { get; } = new()
    {
        Id = Guid.Parse("19c912fb-d961-4e6c-a9a4-80696e35ff12"),
        Name = Roles.CompanyAdmin,
        NormalizedName = Roles.CompanyAdmin.ToUpperInvariant()
    };
}

/// <summary>
/// Provides constants for the platform roles.
/// </summary>
public static class Roles
{
    /// <summary>
    /// The Super Admin role name.
    /// </summary>
    /// <remarks>
    /// This role is assigned to users who are administrators of the platform.
    /// </remarks>
    public const string SuperAdmin = "SuperAdmin";
    
    /// <summary>
    /// The Company Admin role name.
    /// </summary>
    /// <remarks>
    /// This role is assigned to users who are administrators of a company.
    /// </remarks>
    public const string CompanyAdmin = "CompanyAdmin";
}