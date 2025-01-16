using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace Chat.Business.Extensions;

/// <summary>
/// Provides extensions for LINQ operations (both sync/async).
/// </summary>
public static class LinqExtensions
{
    /// <summary>
    /// Splits an async enumerable into groups of a specified size.
    /// </summary>
    /// <typeparam name="TItem">The type of the items in the collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="size">The size of each group.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The collection split into groups.</returns>
    /// <remarks>
    /// This is tantamount to paging the collection.
    /// </remarks>
    [MustUseReturnValue]
    public static async IAsyncEnumerable<IAsyncEnumerable<TItem>> SplitAsyncWithCancellation<TItem>(
        this IAsyncEnumerable<TItem> source, 
        int size, 
        [EnumeratorCancellation] CancellationToken ct = default
    ) {
        await using IAsyncEnumerator<TItem> enumerator = source.GetAsyncEnumerator(ct);
        
        while (await enumerator.MoveNextAsync(ct))
        {
            yield return enumerator.SplitInternal(size, ct);
        }
    }

    private static async IAsyncEnumerable<TItem> SplitInternal<TItem>(
        this IAsyncEnumerator<TItem> enumerator, 
        int size, 
        [EnumeratorCancellation] CancellationToken ct
    ) {
        int pos = 0;
        while (pos < size && await enumerator.MoveNextAsync(ct))
        {
            yield return enumerator.Current;
            pos++;
        }
    }
}