using Chat.Model;
using Chat.Model.Auth;
using Chat.Model.Messaging;
using Chat.Model.Timestamps;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OpenIddict.EntityFrameworkCore.Models;

namespace Chat.Repository;

/// <summary>
/// Represents the application database context.
/// </summary>
public sealed class PlatformDbContext : IdentityDbContext<User, Role, Guid>
{
    /// <summary>
    /// Company-to-company chat messages sent on the platform.
    /// </summary>
    public DbSet<ChatMessage> ChatMessages { get; init; } = null!;
 
    /// <summary>
    /// Company-to-company chat rooms on the platform.
    /// </summary>
    public DbSet<Model.Messaging.ChatRoom> ChatRooms { get; init; } = null!;

    /// <inheritdoc />
    public PlatformDbContext(DbContextOptions<PlatformDbContext> options) : base(options) { }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        UpdateIdentityTablesNaming(builder);
        UpdateOpenIddictTablesNaming(builder);

        // Anything that implements ICreateTimestamp or IUpdateTimestamp should have their timestamp set.
        foreach (IMutableEntityType entityType in builder.Model.GetEntityTypes().Where(e => typeof(ICreateTimestamp).IsAssignableFrom(e.ClrType)))
        {
            builder.Entity(entityType.ClrType).Property<DateTimeOffset>(nameof(ICreateTimestamp.CreatedAt)).HasDefaultValueSql("now()");
        }
        
        #region Messaging

        builder.Entity<ChatMessage>(entity =>
        {
            entity.HasOne(static m => m.Room)
                .WithMany(static r => r.Messages)
                .HasForeignKey(static m => m.RoomId);
            
            entity.HasOne(static m => m.Author)
                .WithMany()
                .HasForeignKey(static m => m.AuthorId);
        });

        builder.Entity<ChatRoom>(entity =>
        {
            entity.HasMany(static r => r.Participants)
                .WithMany();
        });
        
        #endregion // Messaging
        
        #region Role
            
        builder.Entity<Role>()
            .HasData([Role.SuperAdmin, Role.CompanyAdmin]);

        #endregion // Role
    }

    /// <summary>
    /// Updates the Identity tables naming to match Postgres naming conventions.
    /// </summary>
    private static void UpdateIdentityTablesNaming(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("users");
        builder.Entity<IdentityUserToken<Guid>>().ToTable("user_tokens");
        builder.Entity<IdentityUserLogin<Guid>>().ToTable("user_logins");
        builder.Entity<IdentityUserClaim<Guid>>().ToTable("user_claims");
        builder.Entity<Role>().ToTable("roles");
        builder.Entity<IdentityUserRole<Guid>>().ToTable("user_roles");
        builder.Entity<IdentityRoleClaim<Guid>>().ToTable("role_claims");
    }
    
    /// <summary>
    /// Updates the OpenIddict tables naming to match Postgres naming conventions.
    /// </summary>
    private static void UpdateOpenIddictTablesNaming(ModelBuilder builder)
    {
        builder.Entity<OpenIddictEntityFrameworkCoreApplication<Guid>>().ToTable("openiddict_applications");
        builder.Entity<OpenIddictEntityFrameworkCoreAuthorization<Guid>>().ToTable("openiddict_authorizations");
        builder.Entity<OpenIddictEntityFrameworkCoreScope<Guid>>().ToTable("openiddict_scopes");
        builder.Entity<OpenIddictEntityFrameworkCoreToken<Guid>>().ToTable("openiddict_tokens");
    }
}