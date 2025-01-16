using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Chat.Startup.Infrastructure.Swagger;

/// <summary>
/// Contains extension methods for configuring Swagger.
/// </summary>
/// <remarks>
/// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
/// </remarks>
public static class SwaggerExtensions
{
    /// <summary>
    /// Adds Swagger services and app-specific configuration to the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to which Swagger services should be added.</param>
    /// <returns>The <see cref="IServiceCollection"/> instance.</returns>
    public static IServiceCollection AddApplicationSwaggerGen(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            // See: https://learn.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-8.0&tabs=net-cli#xml-comments
            options.IncludeXmlCommentsFromReferencedAssemblies("MedHubCompany.*.xml");
            
            options.SwaggerDoc("v1", new() { Title = "ChatRoom API", Version = "1.0" });
            
            // Authentication: Use external OIDC provider (Microsoft & Google) via /api/auth/login/{provider}
            options.AddSecurityDefinition("Microsoft", new()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    AuthorizationCode = new()
                    {
                        AuthorizationUrl = new("/api/auth/login/Microsoft", UriKind.Relative),
                        TokenUrl = new("/api/auth/callback/Microsoft", UriKind.Relative),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "Profile" },
                            { "email", "Email" }
                        }
                    }
                }
            });
            
            options.AddSecurityDefinition("Google", new()
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new()
                {
                    AuthorizationCode = new()
                    {
                        AuthorizationUrl = new("/api/auth/login/Google", UriKind.Relative),
                        TokenUrl = new("/api/auth/callback/Google", UriKind.Relative),
                        Scopes = new Dictionary<string, string>
                        {
                            { "openid", "OpenID" },
                            { "profile", "Profile" },
                            { "email", "Email" }
                        }
                    }
                }
            });
        });

        return services;
    }
    
    /// <summary>
    /// Adds the necessary and well-configured Swagger components to the application's pipeline.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to which Swagger components should be added.</param>
    /// <returns>The <see cref="WebApplication"/> instance.</returns>
    public static WebApplication UseAppSwagger(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "ChatRoom API v1");
        });

        return app;
    }

    /// <summary>
    /// Includes XML comments from any assemblies referenced by the application.
    /// </summary>
    /// <param name="options">The <see cref="SwaggerGenOptions"/> instance to which XML comments should be added.</param>
    /// <param name="searchPattern">The search pattern to use when looking for XML comments files.</param>
    /// <param name="includeControllerXmlComments">Whether to include XML comments from controller classes.</param>
    /// <returns>The <see cref="SwaggerGenOptions"/> instance.</returns>
    /// <remarks>
    /// This method is useful when the application is split into multiple projects and the XML comments are in a different project.
    /// </remarks>
    private static void IncludeXmlCommentsFromReferencedAssemblies(
        this SwaggerGenOptions options, 
        string searchPattern = "*.xml",
        bool includeControllerXmlComments = true
    ) {
        foreach (string xmlFile in Directory.GetFiles(AppContext.BaseDirectory, searchPattern, SearchOption.AllDirectories))
        {
            options.IncludeXmlComments(xmlFile, includeControllerXmlComments);
        }
    }
}