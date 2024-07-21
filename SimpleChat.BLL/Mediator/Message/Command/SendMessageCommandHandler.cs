using AutoMapper;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using SimpleChat.BLL.DTO.Message;
using SimpleChat.BLL.Specification.Chats;
using SimpleChat.BLL.Specification.Users;
using SimpleChat.DAL.Models.Messages;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Interfaces.Chats;
using SimpleChat.DAL.Repositories.Interfaces.Messages;


namespace SimpleChat.BLL.Mediator.Messages.Command;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Result<MessageDto>>
{
    private readonly IChatRepository _chatRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<SendMessageCommandHandler> _logger;

    public SendMessageCommandHandler(IUserRepository userRepository,IChatRepository chatRepository, IMessageRepository messageRepository, IMapper mapper, ILogger<SendMessageCommandHandler> logger)
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _mapper = mapper;
        _logger = logger;
        _userRepository = userRepository;
    }

    public async Task<Result<MessageDto>> Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling SendMessageCommand for chatId: {ChatId} and userId: {UserId}", request.Message.ChatId, request.Message.UserId);

        var chat = await _chatRepository.GetFirstOrDefaulAsync(new ChatByIdSpec(request.Message.ChatId));

        if (chat == null)
        {
            var errorMsg = $"Chat not found for chatId: {request.Message.ChatId}";
            _logger.LogWarning(errorMsg);
            return Result.Fail(errorMsg);
        }

        var user = await _userRepository.GetFirstOrDefaulAsync(new FindUserByIdSpec(request.Message.UserId));

        if (user == null)
        {
            var errorMsg = $"User not found for userId: {request.Message.UserId}";
            _logger.LogWarning(errorMsg);
            return Result.Fail(errorMsg);
        }

        var message = _mapper.Map<Message>(request.Message);
        message.Timestamp = DateTime.UtcNow;
        message.Chat = chat;
        message.User = user;

        var newMessage =  await _messageRepository.CreateAsync(message);
        await _messageRepository.SaveChangesAsync();

        var messageDto = _mapper.Map<MessageDto>(newMessage);

        return Result.Ok(messageDto);
    }
}
