using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Delete;

public record DeleteOrLeaveChatCommand(int ChatId, int UserId) : IRequest<Result<ChatDto>>;