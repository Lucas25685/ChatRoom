using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chat.Repository.Lifetime;

/// <summary>
/// Provides extension methods for managing the application services' lifetime and execution flow.
/// </summary>
public static class ServiceLifetimeExtensions
{
    /// <summary>
    /// Asynchronously waits for all database migrations to be applied before continuing.
    /// </summary>
    /// <param name="dbContext">The database context to wait for.</param>
    /// <param name="logger">The logger to use for logging.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    /// <exception cref="OperationCanceledException">The operation was canceled.</exception>
    /// <exception cref="TimeoutException">The operation timed out.</exception>
    public static async Task WaitForMigrationsAsync<TService>(this DbContext dbContext, ILogger<TService> logger, CancellationToken ct = default)
    {
        // Hang until migrations are applied
        while ((await dbContext.Database.GetPendingMigrationsAsync(ct)).Any())
        {
            TimeSpan timeout = TimeSpan.FromSeconds(30);
            logger.LogInformation("Waiting for the database to be ready... (Retrying at {Timeout})", DateTimeOffset.Now.Add(timeout));
            await Task.Delay(timeout, ct);
        }
    }
}
