using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Specification.Chats;
using SimpleChat.DAL.Repositories.Interfaces.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Queries.SearchChat;

public class SearchChatsQueryHandler : IRequestHandler<SearchChatsQuery, Result<List<ChatDto>>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SearchChatsQueryHandler> _logger;

    public SearchChatsQueryHandler(IChatRepository chatRepository, IMapper mapper, ILogger<SearchChatsQueryHandler> logger)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<List<ChatDto>>> Handle(SearchChatsQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling SearchChatsQuery with searchTerm: {SearchTerm}", request.SearchTerm);

        var spec = new SearchChatsSpec(request.SearchTerm);
        var chats = await _chatRepository.GetAllAsync(spec);

        if (chats == null || chats.Count() == 0)
        {
            var errorMsg = $"No chats found for searchTerm: {request.SearchTerm}";
            _logger.LogInformation(errorMsg);
        }

        var chatDtos = _mapper.Map<List<ChatDto>>(chats);

        return Result.Ok(chatDtos);
    }
}
