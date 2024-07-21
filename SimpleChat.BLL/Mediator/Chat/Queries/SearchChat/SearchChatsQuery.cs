using FluentResults;
using MediatR;
using SimpleChat.BLL.DTO.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Queries.SearchChat;

public record SearchChatsQuery(string SearchTerm) : IRequest<Result<List<ChatDto>>>;