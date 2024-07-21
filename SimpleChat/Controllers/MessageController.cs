using MediatR;
using Microsoft.AspNetCore.Mvc;
using SimpleChat.BLL.DTO.Message;
using SimpleChat.BLL.Mediator.Messages.Command;
using SimpleChat.WebApi.Services.Interfaces;

namespace SimpleChat.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IChatService _chatService;
    public MessageController(IMediator mediator, IChatService chatService)
    {
        _mediator = mediator;
        _chatService = chatService;
    }

    [HttpPost("messages")]
    public async Task<IActionResult> SendMessage([FromBody] MessageDto messageDto)
    {
        var result = await _mediator.Send(new SendMessageCommand(messageDto));
        if (result.IsSuccess)
        {
           await _chatService.NotifyMessageSentAsync(messageDto);
            return Ok(result.Value);
        }

        return BadRequest(result.Errors);
    }
}