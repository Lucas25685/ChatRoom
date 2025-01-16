using Chat.Business.Persistance;
using Chat.Model;
using Chat.Model.Auth;
using Chat.Repository;
using Chat.Repository.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Throw;

namespace Chat.Startup.Infrastructure.Authentication;

/// <summary>
/// Contains extension methods for configuring authentication.
/// </summary>
public static class AuthenticationExtensions
{
    /// <summary>
    /// The default expiration time for authentication cookies.
    /// </summary>
    public static TimeSpan DefaultCookieExpiration { get; } = TimeSpan.FromDays(1);
    
    /// <summary>
    /// Adds authentication-related services to the specified <see cref="IServiceCollection"/>.
    /// Services include OpenIddict, AspNetCore Identity and Authentication.
    /// </summary>
    public static WebApplicationBuilder AddOpenIddictAuthentication(this WebApplicationBuilder builder)
    {
        IConfiguration config = builder.Configuration.GetSection("Authentication");

        builder.Services.AddIdentity<User, Role>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 8;

                options.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<PlatformDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddOpenIddict()
            .AddCore(static options =>
            {
                options.UseEntityFrameworkCore()
                    .UseDbContext<PlatformDbContext>()
                    .ReplaceDefaultEntities<Guid>();

                options.UseQuartz()
                    .SetMaximumRefireCount(4);
            })
            .AddClient(options =>
            {
                options.AllowAuthorizationCodeFlow();

                options.UseAspNetCore()
                    .EnableStatusCodePagesIntegration()
                    .EnableRedirectionEndpointPassthrough()
                    .DisableTransportSecurityRequirement(); // All API instances are deployed behind a proxy.


                options.UseWebProviders()
                    .AddMicrosoft(ms =>
                    {
                        IConfigurationSection msConfig = config.GetSection("Microsoft");

                        ms.SetClientId(msConfig.GetValue<string>("ClientId").ThrowIfNull());
                        ms.SetClientSecret(msConfig.GetValue<string>("ClientSecret").ThrowIfNull());
                        ms.SetRedirectUri("/api/callback/login/Microsoft");
                        ms.AddScopes("openid", "profile", "email");
                    })
                    .AddGoogle(g =>
                    {
                        IConfigurationSection gConfig = config.GetSection("Google");

                        g.SetClientId(gConfig.GetValue<string>("ClientId").ThrowIfNull());
                        g.SetClientSecret(gConfig.GetValue<string>("ClientSecret").ThrowIfNull());
                        g.SetRedirectUri("/api/callback/login/Google");
                        g.AddScopes("openid", "profile", "email");
                    });

                options.UseSystemNetHttp();

                options.AddDevelopmentEncryptionCertificate();
                options.AddDevelopmentSigningCertificate();
            })
            .AddValidation(iddictValidationBuilder =>
            {
                iddictValidationBuilder.UseLocalServer();
                iddictValidationBuilder.UseAspNetCore();
            });

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                if (config.GetSection("Cookies") is { } authConfig)
                {
                    options.Cookie.Domain = authConfig.GetValue<string>("Domain");
                }

                options.Cookie.IsEssential = true;
                options.ExpireTimeSpan = DefaultCookieExpiration;
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = builder.Environment.IsDevelopment() ? CookieSecurePolicy.None : CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Lax;
                options.Cookie.HttpOnly = false;
                
                // Disable redirection to /Account/Login on challenge for API requests
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                
                // Disable redirection to /Account/AccessDenied on challenge for API requests
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });

        builder.Services.AddScoped<IUserPersistance, UserRepository>();
        return builder;
    }
}