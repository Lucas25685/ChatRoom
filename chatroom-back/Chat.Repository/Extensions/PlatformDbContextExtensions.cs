using System.Data;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Chat.Repository.Extensions;

/// <summary>
/// Extensions methods  for the PlatformDbContext.
/// </summary>
[PublicAPI]
public static class PlatformDbContextExtensions
{
    /// <summary>
    /// Migrates the application's database to the latest version.
    /// </summary>
    public static async Task MigrateDbAsync(this PlatformDbContext context, CancellationToken ct = default)
    {
        await context.Database.MigrateAsync(ct);

        NpgsqlConnection npgsqlConnection = (NpgsqlConnection)context.Database.GetDbConnection();

        if (npgsqlConnection.State is not ConnectionState.Open)
            await npgsqlConnection.OpenAsync(ct);
        
        await npgsqlConnection.ReloadTypesAsync();
        
        await npgsqlConnection.CloseAsync();
    }

    /// <summary>
    /// Configures the application's <see cref="DbContext"/> using the specified <paramref name="dataSourceBuilder"/>.
    /// </summary>
    /// <param name="options">The context's options.</param>
    /// <param name="dataSourceBuilder">The data source builder.</param>
    public static DbContextOptionsBuilder ConfigureDbContext(
        this DbContextOptionsBuilder options, 
        NpgsqlDataSourceBuilder dataSourceBuilder
    ) {
        options.UseNpgsql(dataSourceBuilder.Build(), static options =>
        {
#if !DEBUG
                options.EnableRetryOnFailure();
#endif
        });
        
        options.UseOpenIddict<Guid>();
            
        options.UseSnakeCaseNamingConvention();
        options.UseAllCheckConstraints();
        
#if DEBUG
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
#endif

        return options;
    }
}