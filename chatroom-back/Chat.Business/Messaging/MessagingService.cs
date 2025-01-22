using System.Text.Json;
using System.Text.Json.Serialization;
using Chat.Business.Persistance;
using Chat.Model;
using Chat.Model.Messaging;
using Microsoft.Extensions.Logging;

namespace Chat.Business.Messaging;

/// <summary>
/// Provides functionality for sending messages between companies.
/// </summary>
public sealed class MessagingService
{
    private readonly IMessagingPersistance _messagingPersistance;
    private readonly IMessagingNotificationHandler _notificationHandler;
    private readonly IUserPersistance _userPersistance;
    private readonly ILogger<MessagingService> Logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingService"/> class.
    /// </summary>
    public MessagingService(IMessagingPersistance messagingPersistance,
        IMessagingNotificationHandler notificationHandler,
        IUserPersistance userPersistance,
        ILogger<MessagingService> logger)
    {
        _messagingPersistance = messagingPersistance;
        _notificationHandler = notificationHandler;
        _userPersistance = userPersistance;
        Logger = logger;
    }

    /// <summary>
    /// Gets all messages.
    /// </summary>
    /// <returns>All messages.</returns>
    public async Task<IEnumerable<ChatMessage>> GetMessagesAsync(Guid roomId, CancellationToken ct) => 
        await _messagingPersistance.GetMessages(roomId, ct);

    /// <summary>
    /// Gets all chat rooms.
    /// </summary>
    /// <returns>All chat rooms.</returns>
    public IQueryable<Model.Messaging.ChatRoom> GetRooms() => _messagingPersistance.GetRooms();

    /// <summary>
    /// Gets all messages in a chat room.
    /// </summary>
    /// <param name="roomId">ID of the chat room.</param>
    /// <returns>All messages in the chat room.</returns>
    public IEnumerable<ChatMessage> GetMessagesInRoom(Guid roomId) =>
        _messagingPersistance.GetMessagesInRoom(roomId);

    /// <summary>
    /// Submits a new chat message.
    /// </summary>
    public async Task SubmitMessageAsync(string roomId, string message, string nameIdentifier, CancellationToken ct = default)
    {
        Model.Messaging.ChatRoom chatRoom = await _messagingPersistance.GetChatRoomAsync(Guid.Parse(roomId), ct) 
                                            ?? throw new ArgumentException($"Room {roomId} not found");
        
        User user = await GetUserFromNameIdentifier(nameIdentifier);
        
        ChatMessage chatMessage = new ChatMessage()
        {
            Content = message,
            CreatedAt = DateTime.UtcNow,
            AuthorId = user.Id,
            Author = user,
            Room = chatRoom,
            RoomId = chatRoom.Id
        };
        
        Logger.LogDebug("Received a chat message: {chatMessage}", JsonSerializer.Serialize(chatMessage, new JsonSerializerOptions { WriteIndented = true, ReferenceHandler = ReferenceHandler.Preserve }));
        
        await _messagingPersistance.SubmitMessageAsync(chatMessage, ct);
        
        await _notificationHandler.NotifyNewMessageAsync(chatMessage);
    }

    /// <summary>
    ///  Get user from name identifier.
    /// </summary>
    private async Task<User> GetUserFromNameIdentifier(string nameIdentifier)
    {
        User user = await _userPersistance.GetUserByUsernameAsync(nameIdentifier) 
                    ?? throw new ArgumentException($"User {nameIdentifier} not found");
        return user;
    }

    /// <summary>
    /// create chat room for an offer.
    /// </summary>
    /// <returns>The chat room.</returns>
    public async Task<ChatRoom?> GetChatRoom(Guid roomId,
        CancellationToken ct = default)
    {
        ChatRoom? chatRoom = await _messagingPersistance.GetChatRoomAsync(roomId, ct);

        return chatRoom;
    }
    
    /// <summary>
    /// create chat room for an offer.
    /// </summary>
    /// <returns>The chat room.</returns>
    public async Task<Model.Messaging.ChatRoom?> CreateChatRoom( string nameIdentifier,
        CancellationToken ct = default)
    {
        User user = await GetUserFromNameIdentifier(nameIdentifier);

        // using a HashSet to avoid duplicates if userCompany is importer or exporter
        HashSet<User> participants = [user];
        
        ChatRoom chatRoom = new Model.Messaging.ChatRoom
        {
            Participants = participants.ToList()
        };

        return await _messagingPersistance.CreateRoomAsync(chatRoom, ct);
    }

    /// <summary>
    /// Adds a user to a chat room.
    /// </summary>
    /// <param name="roomId">The ID of the chat room.</param>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The updated chat room.</returns>
    public async Task<ChatRoom> JoinChatRoomAsync(Guid roomId, string userId, CancellationToken ct = default)
    {
        var user = _userPersistance.GetUserById(Guid.Parse(userId))
                    ?? throw new ArgumentException("User not found.");

        var room = await _messagingPersistance.GetChatRoomAsync(roomId, ct)
                    ?? throw new ArgumentException("Room not found.");

        if (room.Participants.Any(p => p.Id == user.Id))
        {
            throw new InvalidOperationException("User already in the room.");
        }

        room.Participants.Add(user);
        await _messagingPersistance.CreateRoomAsync(room, ct);

        return room;
    }


    /// <summary>
    /// Gets a specific chat room.
    /// </summary>
    /// <returns>The chat room.</returns>
    public async Task<Model.Messaging.ChatRoom?> GetChatRoomAsync(Guid roomId, CancellationToken ct = default)
        => await _messagingPersistance.GetChatRoomAsync(roomId, ct);
        
    /// <summary>
    /// Gets a specific chat message.
    /// </summary>
    /// <param name="id">ID of the chat message.</param>
    /// <param name="ct">Cancellation token.</param>
    /// <returns>The chat message.</returns>
    public async Task<ChatMessage?> GetMessageAsync(Guid id, CancellationToken ct = default) =>
        await _messagingPersistance.GetMessageAsync(id, ct);
    
    /// <summary>
    /// Gets all chat rooms with their participants.
    /// </summary>
    /// <returns>All chat rooms with their participants.</returns>
    public async Task<List<Model.Messaging.ChatRoom>> GetAllChatRooms(CancellationToken ct = default)
    {
        return await _messagingPersistance.GetAllChatRooms(ct);
    }
}