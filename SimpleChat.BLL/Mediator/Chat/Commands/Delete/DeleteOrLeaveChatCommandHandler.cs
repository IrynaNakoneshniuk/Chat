using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Specification.Chats;
using SimpleChat.DAL.Repositories.Interfaces.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Delete;

public class DeleteOrLeaveChatCommandHandler : IRequestHandler<DeleteOrLeaveChatCommand, Result<ChatDto>>
{
    private readonly IChatRepository _chatRepository;
    private readonly ILogger<DeleteOrLeaveChatCommandHandler> _logger;
    private readonly IMapper _mapper;

    public DeleteOrLeaveChatCommandHandler(IMapper mapper, IChatRepository chatRepository, ILogger<DeleteOrLeaveChatCommandHandler> logger)
    {
        _chatRepository = chatRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ChatDto>> Handle(DeleteOrLeaveChatCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling DeleteOrLeaveChatCommand for chatId: {ChatId} and userId: {UserId}", request.ChatId, request.UserId);

        var chat = await _chatRepository.GetFirstOrDefaulAsync(new ChatByIdSpec(request.ChatId));
        var chatDto = _mapper.Map<ChatDto>(chat);

        if (chat == null)
        {
            var errorMsg = $"Chat not found for chatId: {request.ChatId}";
            _logger.LogWarning(errorMsg);
            return Result.Fail(errorMsg);
        }

        if (chat.CreatedByUserId == request.UserId)
        {
            _chatRepository.Delete(chat);
            await _chatRepository.SaveChangesAsync();

            _logger.LogInformation("Successfully deleted chatId: {ChatId} by userId: {UserId}", request.ChatId, request.UserId);
        }
        else
        {
            var participant = chat.ChatParticipant.FirstOrDefault(cp => cp.UserId == request.UserId);
            if (participant != null)
            {
                chat.ChatParticipant.Remove(participant);
                _chatRepository.Update(chat);
               await _chatRepository.SaveChangesAsync();

                _logger.LogInformation("UserId: {UserId} left chatId: {ChatId}", request.UserId, request.ChatId);
            }
            else
            {
                var errorMsg = $"UserId: {request.UserId} is not a participant of the chatId: {request.ChatId}";
                _logger.LogWarning(errorMsg);
                return Result.Fail(errorMsg);
            }
        }

        return Result.Ok(chatDto);
    }
}
