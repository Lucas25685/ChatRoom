namespace Chat.Startup.Middlewares;

/// <summary>
/// Provides extension methods for configuring and using middlewares.
/// </summary>
public static class MiddlewareExtensions
{
    /// <summary>
    /// Adds the HTTP protocol rewrite middleware to the service collection.
    /// </summary>
    /// <remarks>
    /// This middleware rewrites the request scheme to HTTPS on non-dev/remote environments.
    /// </remarks>
    /// <param name="builder">The application builder.</param>
    /// <returns>The service collection.</returns>
    /// <seealso cref="HttpProtocolRewriteMiddleware"/>
    public static WebApplicationBuilder AddHttpProtocolRewriteMiddleware(this WebApplicationBuilder builder)
    {
        if (!builder.Environment.IsDevelopment())
        {
            builder.Services.AddSingleton<HttpProtocolRewriteMiddleware>();
        }
        
        return builder;
    }
    
    /// <summary>
    /// Uses the HTTP protocol rewrite middleware.
    /// </summary>
    /// <remarks>
    /// This middleware rewrites the request scheme to HTTPS on non-dev/remote environments.
    /// </remarks>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    /// <seealso cref="HttpProtocolRewriteMiddleware"/>
    public static IApplicationBuilder UseHttpProtocolRewrite(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            app.UseMiddleware<HttpProtocolRewriteMiddleware>();
        }
        
        return app;
    }
}