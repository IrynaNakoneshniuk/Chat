using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Specification.Users;
using SimpleChat.DAL.Models.ChatParticipants;
using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Interfaces.Chats;

namespace SimpleChat.BLL.Mediator.Chats.Commands.Create;

public class CreateChatCommandHandler : IRequestHandler<CreateChatCommand, Result<ChatDto>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<CreateChatCommandHandler> _logger;
    private readonly IMapper _mapper;

    public CreateChatCommandHandler(
        IChatRepository chatRepository,
        IUserRepository userRepository,
        ILogger<CreateChatCommandHandler> logger,
        IMapper mapper)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<Result<ChatDto>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        if (request.NewChat == null)
        {
            return Result.Fail<ChatDto>("Chat details cannot be null.");
        }

        var user = await GetUserById(request.NewChat.CreatedByUserId);
        if (user == null)
        {
            return Result.Fail<ChatDto>("User not found.");
        }

        var chat = await CreateChatAsync(request.NewChat, user);
        var participants = await CreateParticipantsAsync(chat, request.NewChat.ParticipantIds);

        if (participants.Count == 0)
        {
            return Result.Fail<ChatDto>("No valid participants found.");
        }

        chat.ChatParticipant = participants;

        if (await SaveChangesAsync() == 0)
        {
            return Result.Fail<ChatDto>("Error saving chat to the database.");
        }

        var chatDto = _mapper.Map<ChatDto>(chat);
        return Result.Ok(chatDto);
    }

    private async Task<User?> GetUserById(int userId)
    {
        return await _userRepository.GetFirstOrDefaulAsync(new FindUserByIdSpec(userId));
    }

    private async Task<Chat> CreateChatAsync(CreateChatDto chatDto, User user)
    {
        var chat = _mapper.Map<Chat>(chatDto);
        chat.CreatedByUser = user;
        return await _chatRepository.CreateAsync(chat);
    }

    private async Task<List<ChatParticipant>> CreateParticipantsAsync(Chat chat, List<int> participantIds)
    {
        var participants = new List<ChatParticipant>();
        foreach (var participantId in participantIds)
        {
            var participant = await GetUserById(participantId);
            if (participant == null)
            {
                var errorMsg = $"User with ID {participantId} not found.";
                _logger.LogWarning(errorMsg);
                continue;
            }

            participants.Add(new ChatParticipant
            {
                Chat = chat,
                User = participant
            });
        }

        return participants;
    }

    private async Task<int> SaveChangesAsync()
    {
        return await _chatRepository.SaveChangesAsync();
    }
}
