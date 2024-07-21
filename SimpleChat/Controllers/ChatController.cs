using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Mediator.Chats.Commands.Create;
using SimpleChat.BLL.Mediator.Chats.Commands.Delete;
using SimpleChat.BLL.Mediator.Chats.Queries.GetAllByUserId;
using SimpleChat.BLL.Mediator.Chats.Queries.SearchChat;
using SimpleChat.WebApi.Services.Interfaces;


namespace SimpleChat.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IChatService _chatService;
    public ChatController(IMediator mediator, IChatService chatService)
    {
        _mediator = mediator;
        _chatService = chatService;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllChatsByUserId(int userId)
    {
        var result = await _mediator.Send(new GetAllByIdQuery(userId));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }

    [HttpPost]
    public async Task<IActionResult> CreateChat([FromBody] CreateChatDto newChat)
    {
        var result = await _mediator.Send(new CreateChatCommand(newChat));

        if (result.IsSuccess)
        {
           await _chatService.CreateChatInGroupAsync(result.Value);
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }


    [HttpDelete("{chatId}")]
    public async Task<IActionResult> DeleteOrLeaveChat(int chatId, [FromQuery] int userId)
    {
        var result = await _mediator.Send(new DeleteOrLeaveChatCommand(chatId, userId));

        if (result.IsSuccess)
        {
            await _chatService.HandleChatDeletionOrLeaveAsync(result.Value, userId);
            return Ok();
        }

        return BadRequest(result.Errors);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchChats([FromQuery] string searchTerm)
    {
        var result = await _mediator.Send(new SearchChatsQuery(searchTerm));

        if (result.IsSuccess)
        {
            return Ok(result.Value);
        }

        return NotFound(result.Errors);
    }
}
