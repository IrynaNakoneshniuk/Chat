using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Specification.Chats;
using SimpleChat.DAL.Repositories.Interfaces.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Queries.GetAllByUserId;

public class GetAllByIdQueryHandler : IRequestHandler<GetAllByIdQuery, Result<List<ChatDto>>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllByIdQueryHandler> _logger;

    public GetAllByIdQueryHandler(IChatRepository chatRepository, IMapper mapper, ILogger<GetAllByIdQueryHandler> logger)
    {
        _chatRepository = chatRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Result<List<ChatDto>>> Handle(GetAllByIdQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling GetAllByIdQuery for userId: {UserId}", request.UserId);

        var spec = new ChatsByUserIdSpec(request.UserId);
        var chats = await _chatRepository.GetAllAsync(spec);

        if (chats == null || !chats.Any())
        {
            var errorMsg = $"No chats found for userId: {request.UserId}";
            _logger.LogWarning(errorMsg);
            return Result.Fail(errorMsg);
        }

        var chatDtos = _mapper.Map<List<ChatDto>>(chats);

        _logger.LogInformation("Successfully retrieved {ChatCount} chats for userId: {UserId}", chatDtos.Count, request.UserId);
        return Result.Ok(chatDtos);
    }
}