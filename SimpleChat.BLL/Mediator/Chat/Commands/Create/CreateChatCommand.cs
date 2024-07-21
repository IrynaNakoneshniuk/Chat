using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Create;
public record CreateChatCommand(CreateChatDto NewChat) : IRequest<Result<ChatDto>>;