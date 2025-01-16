using Mapster;
using MapsterMapper;
using Chat.ApiModel.Messaging;
using Chat.Business.Messaging;
using Chat.Model.Messaging;
using Microsoft.AspNetCore.Mvc;

namespace Chat.Api.Controllers;

/// <summary>
/// Controller for messaging-related functionality.
/// </summary>
[ApiController, Route("api/messaging/")]
public sealed class MessagingController : UserControllerBase
{
    private readonly MessagingService _messagingService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessagingController"/> class.
    /// </summary>
    public MessagingController(MessagingService messagingService, IMapper mapper)
    {
        _messagingService = messagingService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets all messages in a chat room.
    /// </summary>
    /// <param name="roomId">ID of the chat room.</param>
    /// <returns>All messages.</returns>
    /// <response code="200">Messages retrieved successfully.</response>
    /// <response code="404">Chat room not found.</response>
    /// <response code="403">Unauthorized to view messages.</response>
    [HttpGet("room/{roomId:guid}/messages")]
    public async Task<ActionResult<IEnumerable<ChatMessageDto>>> GetMessagesForRoomAsync(Guid roomId)
    {
        // Ensure the chat room exists.
        IEnumerable<ChatMessage> messages = await _messagingService.GetMessagesAsync(roomId, HttpContext.RequestAborted);

        return Ok(messages.Adapt<IEnumerable<ChatMessageDto>>());
    }

    /// <summary>
    /// Gets a singular message.
    /// </summary>
    /// <param name="id">ID of the message.</param>
    /// <returns>The message.</returns>
    /// <response code="200">Message retrieved successfully.</response>
    /// <response code="404">Message not found.</response>
    /// <response code="403">Unauthorized to view message.</response>
    [HttpGet("message/{id:guid}")]
    public async Task<ActionResult<ChatMessageDto>> GetMessageAsync(Guid id)
    {
        if (await _messagingService.GetMessageAsync(id) is not { } message)
        {
            return NotFound();
        }

        return _mapper.Map<ChatMessageDto>(message);
    }
}