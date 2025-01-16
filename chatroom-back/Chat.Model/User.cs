using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Chat.Model;

/// <summary>
/// Represents a platform user.
/// </summary>
public sealed class User : IdentityUser<Guid>
{
    /// <summary>
    /// The user's first name.
    /// </summary>
    [ProtectedPersonalData, MaxLength(256)]
    public string FirstName { get; set; } = "";
    
    /// <summary>
    /// The user's last name.
    /// </summary>
    [ProtectedPersonalData, MaxLength(256)]
    public string LastName { get; set; } = "";
}