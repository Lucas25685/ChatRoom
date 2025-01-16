using Polly;

namespace Chat.Startup.Infrastructure.Resilience;

/// <summary>
/// Provides extension methods for resilience.
/// </summary>
public static class ResilienceExtensions
{
    /// <summary>
    /// Key names for resilience policies.
    /// </summary>
    public static class Keys
    {
        /// <summary>
        /// The long running retry policy key.
        /// </summary>
        public const string LongRunningRetry = "Retry-LongRunning";
    }

    /// <summary>
    /// Adds the application resilience policies to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the policies to.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddApplicationResilience(this IServiceCollection services)
    {
        // Long running retry policy : Wait 1 minute, scale up to 10 minutes
        services.AddResiliencePipeline(Keys.LongRunningRetry, builder => builder.AddRetry(new()
        {
            Delay = TimeSpan.FromMinutes(1),
            MaxDelay = TimeSpan.FromMinutes(10),
            BackoffType = DelayBackoffType.Linear
        }));

        services.AddResilienceEnricher();
        
        return services;
    }
}