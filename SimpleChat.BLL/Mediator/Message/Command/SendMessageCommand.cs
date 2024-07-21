using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.Message;

namespace SimpleChat.BLL.Mediator.Messages.Command;

public record SendMessageCommand(MessageDto Message) : IRequest<Result<MessageDto>>;