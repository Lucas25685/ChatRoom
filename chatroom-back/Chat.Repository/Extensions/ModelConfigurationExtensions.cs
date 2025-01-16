using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Repository.Extensions;

/// <summary>
/// Contains extension methods for configuring the model.
/// </summary>
public static class ModelConfigurationExtensions
{
    /// <summary>
    /// Configures the specified property as a JSON column.
    /// </summary>
    /// <param name="entity">The entity builder.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <typeparam name="TEntity">The CLR type of the owned entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <returns>The property builder.</returns>
    public static EntityTypeBuilder<TEntity> HasJsonColumn<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity,TProperty?>> propertyExpression)
        where TEntity : class
        where TProperty : class
    {
        return entity.OwnsOne(
            propertyExpression,
            static p => p.ToJson()
        );
    }
    
    /// <summary>
    /// Configures the specified property as a JSON column.
    /// </summary>
    /// <param name="entity">The entity builder.</param>
    /// <param name="propertyExpression">The property expression.</param>
    /// <typeparam name="TEntity">The CLR type of the owned entity.</typeparam>
    /// <typeparam name="TProperty">The type of the property.</typeparam>
    /// <returns>The property builder.</returns>
    /// <remarks>
    /// This overload is used for enumerables.
    /// </remarks>
    public static EntityTypeBuilder<TEntity> HasJsonEnumerableColumn<TEntity, TProperty>(this EntityTypeBuilder<TEntity> entity, Expression<Func<TEntity,IEnumerable<TProperty>?>> propertyExpression)
        where TEntity : class
        where TProperty : class
    {
        return entity.OwnsMany(
            propertyExpression,
            static p => p.ToJson()
        );
    }
}