using System.Security.Claims;
using Chat.Business.Persistance;
using Chat.Startup.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Client.AspNetCore;
using Throw;

namespace Chat.Startup.Controllers;

/// <summary>
/// Provides a controller for handling authentication requests.
/// </summary>
[ApiController]
public sealed class AuthenticationController : ControllerBase
{
    private readonly IUserPersistance _userPersistance;

    /// <summary>
    /// Represents the entry point of the application.
    /// </summary>
    public AuthenticationController(IUserPersistance userPersistance)
    {
        _userPersistance = userPersistance;
    }

    /// <summary>
    /// Challenges the specified provider.
    /// </summary>
    /// <param name="provider">The provider to challenge.</param>
    /// <param name="redirectUri">The URI to redirect to after the authentication flow completes.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the specified provider is not supported.</exception>
    /// <remarks>
    /// This action is used to trigger the authentication flow for the specified provider.
    /// </remarks>
    /// <response code="400">Thrown when the specified provider is not supported.</response>
    /// <response code="500">Thrown when an unexpected error occurs.</response>
    [HttpGet("/api/auth/login/{provider}")]
    public ChallengeResult LoginAsync(string provider, string? redirectUri)
    {
        // Note: the redirect URI is set to /api/callback/login/{provider}.
        // This URI must be registered as a valid redirect URI in the corresponding provider's application settings.
        ChallengeResult challengeResult = Challenge(new AuthenticationProperties
        {
            RedirectUri = Url.Action("LoginCallback", new { provider, redirectUri })
        }, provider);
        
        // Set redirectUri to cookies if not null
        if (redirectUri is not null)
        {
            Response.Cookies.Append("Auth_RedirectUri", redirectUri);
        }
        
        return challengeResult;
    }
    
    // Note: this controller uses the same callback action for all providers
    // but for users who prefer using a different action per provider,
    // the following action can be split into separate actions.
    /// <summary>
    /// Handles the callback from the specified provider.
    /// </summary>
    /// <param name="provider">The provider to handle the callback for.</param>
    /// <param name="redirectUri">The URI to redirect to after the authentication flow completes.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <exception cref="InvalidOperationException">Thrown when the external authorization data cannot be used for authentication.</exception>
    [HttpGet("/api/callback/login/{provider}"), HttpPost("/api/callback/login/{provider}"), IgnoreAntiforgeryToken]
    public async Task<IActionResult> LoginCallbackAsync(string provider, string? redirectUri)
    {
        // Retrieve the authorization data validated by OpenIddict as part of the callback handling.
        AuthenticateResult result = await HttpContext.AuthenticateAsync(OpenIddictClientAspNetCoreDefaults.AuthenticationScheme);

        // Multiple strategies exist to handle OAuth 2.0/OpenID Connect callbacks, each with their pros and cons:
        //
        //   * Directly using the tokens to perform the necessary action(s) on behalf of the user, which is suitable
        //     for applications that don't need a long-term access to the user's resources or don't want to store
        //     access/refresh tokens in a database or in an authentication cookie (which has security implications).
        //     It is also suitable for applications that don't need to authenticate users but only need to perform
        //     action(s) on their behalf by making API calls using the access token returned by the remote server.
        //
        //   * Storing the external claims/tokens in a database (and optionally keeping the essential claims in an
        //     authentication cookie so that cookie size limits are not hit). For the applications that use ASP.NET
        //     Core Identity, the UserManager.SetAuthenticationTokenAsync() API can be used to store external tokens.
        //
        //     Note: in this case, it's recommended to use column encryption to protect the tokens in the database.
        //
        //   * Storing the external claims/tokens in an authentication cookie, which doesn't require having
        //     a user database but may be affected by the cookie size limits enforced by most browser vendors
        //     (e.g Safari for macOS and Safari for iOS/iPadOS enforce a per-domain 4KB limit for all cookies).
        //
        //     Note: this is the approach used here, but the external claims are first filtered to only persist
        //     a few claims like the user identifier. The same approach is used to store the access/refresh tokens.

        // Important: if the remote server doesn't support OpenID Connect and doesn't expose a userinfo endpoint,
        // result.Principal.Identity will represent an unauthenticated identity and won't contain any user claim.
        //
        // Such identities cannot be used as-is to build an authentication cookie in ASP.NET Core (as the
        // antiforgery stack requires at least a name claim to bind CSRF cookies to the user's identity) but
        // the access/refresh tokens can be retrieved using result.Properties.GetTokens() to make API calls.
        if (result.Principal is not { Identity.IsAuthenticated: true })
        {
            throw new InvalidOperationException("The external authorization data cannot be used for authentication.");
        }

        // Check if the tokens are present
        AuthenticationToken[] tokens = result.Properties?.GetTokens().ToArray() ?? [];
        
        // Build an identity based on the external claims and that will be used to create the authentication cookie.
        ClaimsIdentity identity = new(
            authenticationType: $"ExternalLogin:{provider}",
            nameType: ClaimTypes.Name,
            roleType: ClaimTypes.Role
        );

        // HACK: MS auth doesn't always return email. Substitute w/ Name claim.
        if (result.Principal.GetClaim(ClaimTypes.Email) is null)
        {
            result.Principal.SetClaim(ClaimTypes.Email, result.Principal.GetClaim(ClaimTypes.Name));
        }
        
        // By default, OpenIddict will automatically try to map the email/name and name identifier claims from
        // their standard OpenID Connect or provider-specific equivalent, if available. If needed, additional
        // claims can be resolved from the external identity and copied to the final authentication cookie.
        identity.SetClaim(ClaimTypes.Email, result.Principal.GetClaim(ClaimTypes.Email))
                .SetClaim(ClaimTypes.Name, result.Principal.GetClaim(ClaimTypes.Name))
                .SetClaim(ClaimTypes.NameIdentifier, result.Principal.GetClaim(ClaimTypes.NameIdentifier));

        // Preserve the registration details to be able to resolve them later.
        identity.SetClaim(OpenIddictConstants.Claims.Private.RegistrationId, result.Principal.GetClaim(OpenIddictConstants.Claims.Private.RegistrationId))
                .SetClaim(OpenIddictConstants.Claims.Private.ProviderName, result.Principal.GetClaim(OpenIddictConstants.Claims.Private.ProviderName));

        // Important: when using ASP.NET Core Identity and its default UI, the identity created in this action is
        // not directly persisted in the final authentication cookie (called "application cookie" by Identity) but
        // in an intermediate authentication cookie called "external cookie" (the final authentication cookie is
        // later created by Identity's ExternalLogin Razor Page by calling SignInManager.ExternalLoginSignInAsync()).
        //
        // Unfortunately, this process doesn't preserve the claims added here, which prevents flowing claims
        // returned by the external provider down to the final authentication cookie. For scenarios that
        // require that, the claims can be stored in Identity's database by calling UserManager.AddClaimAsync()
        // directly in this action or by scaffolding the ExternalLogin.cshtml page that is part of the default UI:
        // https://learn.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims#add-and-update-user-claims.
        //
        // Alternatively, if flowing the claims from the "external cookie" to the "application cookie" is preferred,
        // the default ExternalLogin.cshtml page provided by Identity can be scaffolded to replace the call to
        // SignInManager.ExternalLoginSignInAsync() by a manual sign-in operation that will preserve the claims.
        // For scenarios where scaffolding the ExternalLogin.cshtml page is not convenient, a custom SignInManager
        // with an overridden SignInOrTwoFactorAsync() method can also be used to tweak the default Identity logic.
        //
        // For more information, see https://haacked.com/archive/2019/07/16/external-claims/ and
        // https://stackoverflow.com/questions/42660568/asp-net-core-identity-extract-and-save-external-login-tokens-and-add-claims-to-l/42670559#42670559.

        string uid = result.Principal.GetClaim(ClaimTypes.NameIdentifier).ThrowIfNull();
        
        if (await _userPersistance.GetUserByUsernameAsync(uid) is not { } user)
        {
            user = await _userPersistance.CreateUserAsync(new()
                {
                    Email = result.Principal.GetClaim(ClaimTypes.Email),
                    UserName = uid,
                    FirstName = result.Principal.GetClaim(ClaimTypes.GivenName) ?? "",
                    LastName = result.Principal.GetClaim(ClaimTypes.Surname) ?? ""
                }, 
                new UserLoginInfo(provider, uid, provider)
            );
        }
        
        // Add the user's roles to the claims.
        foreach (string role in await _userPersistance.GetUserRolesAsync(user.UserName!))
        {
            identity.AddClaim(new(ClaimTypes.Role, role));
        }
        
        // Get redirectUri from cookies if null
        redirectUri ??= Request.Cookies["Auth_RedirectUri"];
        Response.Cookies.Delete("Auth_RedirectUri");
        
        // Build the authentication properties based on the properties that were added when the challenge was triggered.
        AuthenticationProperties properties = new(result.Properties!.Items)
        {
            RedirectUri = redirectUri ?? "/swagger",
            ExpiresUtc = DateTimeOffset.Now.Add(AuthenticationExtensions.DefaultCookieExpiration),
            AllowRefresh = true,
            IsPersistent = true
        };

        // If needed, the tokens returned by the authorization server can be stored in the authentication cookie.
        // To make cookies less heavy, tokens that are not used are filtered out before creating the cookie.

        properties.StoreTokens(tokens.Where(token => token.Name is
            // Preserve the access and refresh tokens returned in the token response, if available.
            OpenIddictClientAspNetCoreConstants.Tokens.BackchannelAccessToken or
            OpenIddictClientAspNetCoreConstants.Tokens.RefreshToken));

        // Ask the default sign-in handler to return a new cookie and redirect the
        // user agent to the return URL stored in the authentication properties.
        //
        // For scenarios where the default sign-in handler configured in the ASP.NET Core
        // authentication options shouldn't be used, a specific scheme can be specified here.
        
        // Write the authentication cookie 
        return SignIn(new(identity), properties, CookieAuthenticationDefaults.AuthenticationScheme);
    }
    
    
    /// <summary>
    /// Logs out the current user and clears the session.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    /// <response code="200">The logout was successful.</response>
    [HttpPost("/api/auth/logout")]
    public async Task<IActionResult> LogoutAsync()
    {
        // Ask ASP.NET Core to delete the authentication cookie.
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

        // Return a simple JSON object indicating success. The client-side application can then decide where to redirect or what action to take.
        return Ok(new { message = "Successfully logged out" });
    }
}