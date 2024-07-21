using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Queries.GetAllByUserId;

public record GetAllByIdQuery(int UserId) : IRequest<Result<List<ChatDto>>>;

