using Chat.Model.Messaging;

namespace Chat.Business.Persistance;


/// <summary>
/// Manages the persistance of company messaging.
/// </summary>
public interface IMessagingPersistance
{
    /// <summary>
    /// Gets messages from a room
    /// </summary>
    /// <returns>All messages of the room.</returns>
    Task<IEnumerable<ChatMessage>> GetMessages( Guid roomId, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all chat rooms.
    /// </summary>
    /// <returns>All chat rooms.</returns>
    IQueryable<Model.Messaging.ChatRoom> GetRooms();
    
    /// <summary>
    /// Gets all messages in a chat room.
    /// </summary>
    /// <param name="roomId">ID of the chat room.</param>
    /// <returns>All messages in the chat room.</returns>
    IEnumerable<ChatMessage> GetMessagesInRoom(Guid roomId);

    /// <summary>
    /// Submits a new chat message.
    /// </summary>
    /// <param name="message">Message to submit.</param>
    /// <param name="ct">Cancellation token.</param>
    Task SubmitMessageAsync(ChatMessage message, CancellationToken ct = default);

    /// <summary>
    /// Creates a new chat room.
    /// </summary>
    /// <param name="room">Room to create.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <exception cref="ArgumentException">Thrown when the participants are invalid.</exception>
    Task<Model.Messaging.ChatRoom> CreateRoomAsync(Model.Messaging.ChatRoom room, CancellationToken ct = default);

    /// <summary>
    /// Gets a specific chat room.
    /// </summary>
    /// <param name="roomId">ID of the chat room.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The chat room.</returns>
    Task<Model.Messaging.ChatRoom?> GetChatRoomAsync(Guid roomId, CancellationToken ct = default);

    /// <summary>
    /// Gets a specific chat message.
    /// </summary>
    /// <param name="id">ID of the chat message.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The chat message.</returns>
    Task<ChatMessage?> GetMessageAsync(Guid id, CancellationToken ct = default);
    
    /// <summary>
    /// Gets all chat rooms with their participants.
    /// </summary>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>All chat rooms.</returns>
    Task<List<ChatRoom>> GetAllChatRooms(CancellationToken ct = default);
}