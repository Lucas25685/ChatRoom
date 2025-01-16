namespace Chat.Model.Timestamps;

/// <summary>
/// Specifies a database entity associated with regular updates.
/// </summary>
public interface ICreateTimestamp
{
    /// <summary>
    /// The date and time of the database entity's creation.
    /// </summary>
    public DateTimeOffset CreatedAt { get; set; }
}