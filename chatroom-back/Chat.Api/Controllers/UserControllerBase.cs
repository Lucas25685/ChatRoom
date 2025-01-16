using System.Security.Authentication;
using Chat.Api.Infrastructure.Authentication;
using Chat.Business.Persistance;
using Chat.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

/// <summary>
/// Provides a user context aware base controller for authenticated endpoints.
/// </summary>
[ApiController, Route("api/user"), Authorize]
public abstract class UserControllerBase : ControllerBase
{
    /// <summary>
    /// The current platform user object.
    /// </summary>
    protected User PlatformUser => HttpContext.RequestServices.GetRequiredService<IUserPersistance>()
                                       .GetUserByUsernameAsync(
                                           HttpContext.User.GetUserUid() ??
                                           throw new AuthenticationException("No UserUid in HttpContext")).GetAwaiter()
                                       .GetResult()
                                   ?? throw new AuthenticationException(
                                       $"No user found for user Id {HttpContext.User.GetUserUid()}");
}