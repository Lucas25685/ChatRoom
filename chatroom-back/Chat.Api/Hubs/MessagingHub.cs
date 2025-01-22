using System.Security.Authentication;
using Mapster;
using MapsterMapper;
using Chat.Api.Infrastructure.Authentication;
using Chat.ApiModel.Messaging;
using Chat.Business.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Chat.Api.Hubs;

// TODO: Implement error handling via filters

/// <summary>
/// Represents the company messaging hub.
/// </summary>
[Authorize]
public sealed class MessagingHub : Hub<IMessagingHubPush>, IMessagingHubInvoke
{
    /// route to the hub
    public static string HubPath => "api/hub/messaging";

    private readonly MessagingService _messagingService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingHub"/> class.
    /// </summary>
    public MessagingHub(MessagingService messagingService, IMapper mapper)
    {
        _messagingService = messagingService;
        _mapper = mapper;
    }

    private string NameIdentifier => Context.User?.GetNameIdentifier() 
        ?? throw new AuthenticationException("User nameidentifier not found in Claims.");
    
    /// <summary>
    /// Gets the chat room from an offer.
    /// </summary>
    public async Task<ChatRoomDto> GetChatRoom(Guid roomId)
    {
        Model.Messaging.ChatRoom room = await _messagingService.GetChatRoom(roomId)
                                        ?? throw new ArgumentException("Offer not found");

        return _mapper.Map<ChatRoomDto>(room);
    }
    
    /// <summary>
    /// Gets the chat room from an offer.
    /// </summary>
    public async Task<ChatRoomDto> CreateChatRoom()
    {
        Model.Messaging.ChatRoom room = await _messagingService.CreateChatRoom(NameIdentifier)
                                        ?? throw new ArgumentException("Offer not created");

        return _mapper.Map<ChatRoomDto>(room);
    }

    /// <summary>
    /// Gets the chat room from an offer.
    /// </summary>
    public async Task<IEnumerable<ChatMessageDto>> JoinChatRoom(Guid roomId)
    {
        var userId = NameIdentifier;
        var room = await _messagingService.JoinChatRoomAsync(roomId, userId, Context.ConnectionAborted);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());

        var messages = _messagingService.GetMessagesInRoom(roomId);

        return messages.Adapt<IEnumerable<ChatMessageDto>>(_mapper.Config);
    }

    /// <inheritdoc />
    public async Task LeaveChatRoom(Guid roomId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
    }

    /// <inheritdoc />
    public async Task SendMessage(string roomId, string message)
    {
        await _messagingService.SubmitMessageAsync(roomId, message, NameIdentifier);
    }

    /// <summary>
    /// Gets all chat rooms.
    /// </summary>
    /// <returns>All chat rooms.</returns>
    public async Task<IEnumerable<ChatRoomDto>> GetAllChatRooms()
{
    var chatRooms = await _messagingService.GetAllChatRooms(Context.ConnectionAborted);
    return _mapper.Map<IEnumerable<ChatRoomDto>>(chatRooms);
}
}