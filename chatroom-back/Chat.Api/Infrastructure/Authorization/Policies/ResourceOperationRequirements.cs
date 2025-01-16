using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Chat.Api.Infrastructure.Authorization.Policies;

/// <summary>
/// Requirement for CRUD operations.
/// </summary>
public static class ResourceOperationRequirements
{
    /// <summary>
    /// Requirement for creating a resource.
    /// </summary>
    public static OperationAuthorizationRequirement Create = new() { Name = ResourceOperationNames.Create };
    
    /// <summary>
    /// Requirement for reading a resource.
    /// </summary>
    public static OperationAuthorizationRequirement Read = new() { Name = ResourceOperationNames.Read };
    
    /// <summary>
    /// Requirement for updating a resource.
    /// </summary>
    public static OperationAuthorizationRequirement Update = new() { Name = ResourceOperationNames.Update };
    
    /// <summary>
    /// Requirement for deleting a resource.
    /// </summary>
    public static OperationAuthorizationRequirement Delete = new() { Name = ResourceOperationNames.Delete };
}