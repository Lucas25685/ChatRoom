namespace Chat.Startup.Middlewares;

/// <summary>
/// Rewrites HttpContext of incoming requests to use the HTTPS protocol.
/// This is used in production environments to ensure that proxied requests hold the right protocol.
/// </summary>
public sealed class HttpProtocolRewriteMiddleware : IMiddleware
{
    /// <summary>
    /// Rewrites the protocol of the incoming request to HTTPS.
    /// </summary>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.Request.Scheme is "http")
        {
            context.Request.Scheme = "https";
        }

        await next(context);
    }
}