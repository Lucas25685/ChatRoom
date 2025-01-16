using Azure;
using Azure.Core;
using Azure.Identity;

namespace Chat.Startup.Infrastructure.Azure;

/// <summary>
/// Retrieves the token for the Azure database.
/// </summary>
public sealed class AzureDatabaseTokenRetriever
{
    private readonly DefaultAzureCredential _credential;

    /// <summary>
    /// Initializes a new instance of the <see cref="AzureDatabaseTokenRetriever"/> class.
    /// </summary>
    public AzureDatabaseTokenRetriever(DefaultAzureCredential credential)
    {
        _credential = credential;
    }
    
    /// <summary>
    /// Retrieves synchronously the token for the Azure database.
    /// </summary>
    /// <returns>The token for the Azure database.</returns>
    /// <exception cref="RequestFailedException">The token could not be retrieved.</exception>
    /// <exception cref="AuthenticationFailedException">The authentication failed.</exception>
    public AccessToken GetToken()
    {
        TokenRequestContext tokenRequestContext = new(["https://ossrdbms-aad.database.windows.net/.default"]);
        return _credential.GetToken(tokenRequestContext, CancellationToken.None);
    }
    
    /// <summary>
    /// Retrieves asynchronously the token for the Azure database.
    /// </summary>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The token for the Azure database.</returns>
    /// <exception cref="RequestFailedException">The token could not be retrieved.</exception>
    /// <exception cref="AuthenticationFailedException">The authentication failed.</exception>
    public async Task<AccessToken> GetTokenAsync(CancellationToken ct = default)
    {
        TokenRequestContext tokenRequestContext = new(["https://ossrdbms-aad.database.windows.net/.default"]);
        return await _credential.GetTokenAsync(tokenRequestContext, ct);
    }
}