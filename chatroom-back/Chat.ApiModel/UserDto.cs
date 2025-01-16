using System.ComponentModel.DataAnnotations;

namespace ChatRoom.ApiModel;

/// <summary>
/// Represents a platform user.
/// </summary>
public sealed class UserDto
{
    /// <summary>
    /// The user's ID.
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// The user's email.
    /// </summary>
    [EmailAddress]
    public string Email { get; set; } = "";
    
    /// <summary>
    /// The user's first name.
    /// </summary>
    public string FirstName { get; set; } = "";
    
    /// <summary>
    /// The user's last name.
    /// </summary>
    public string LastName { get; set; } = "";
    
    /// <summary>
    /// The user's phone number.
    /// </summary>
    [Phone]
    public string PhoneNumber { get; set; } = "";
    
    /// <summary>
    /// The user's roles
    /// </summary>
    public string[] Roles { get; set; } = [];
}