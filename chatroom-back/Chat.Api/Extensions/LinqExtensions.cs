namespace Chat.Api.Extensions;

/// <summary>
/// Provides generic LINQ extensions.
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Filters all non-null values from the sequence.
    /// </summary>
    /// <param name="source">The source sequence.</param>
    /// <typeparam name="TItem">The type of the items in the sequence.</typeparam>
    /// <returns>The filtered sequence.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the source is <see langword="null"/>.</exception>
    public static IQueryable<TItem> FilterNotNull<TItem>(this IQueryable<TItem?> source)
    {
        ArgumentNullException.ThrowIfNull(source);
        return source.Where(item => item != null).Cast<TItem>();
    }
}