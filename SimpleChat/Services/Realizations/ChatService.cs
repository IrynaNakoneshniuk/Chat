using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.DTO.Message;
using SimpleChat.BLL.Specification.Users;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.WebApi.Hubs;
using SimpleChat.WebApi.Services.Interfaces;

namespace SimpleChat.WebApi.Services.Realizations;

public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IUserRepository _userRepository;

    public ChatService(IHubContext<ChatHub> hubContext, IUserRepository userRepository, IMapper mapper)
    {
        _hubContext = hubContext;
        _userRepository = userRepository;
    }

    public async Task CreateChatInGroupAsync(ChatDto chatDto)
    {
        foreach (var participant in chatDto.Participants)
        {
            var user = await _userRepository.GetFirstOrDefaulAsync(new FindUserByIdSpec(participant.Id));
            if (user != null && !string.IsNullOrEmpty(user.ConnectionId))
            {
                await _hubContext.Groups.AddToGroupAsync(user.ConnectionId, chatDto.Id.ToString());
            }
        }

        await _hubContext.Clients.Group(chatDto.Id.ToString()).SendAsync("ChatСreated", chatDto);
    }

    public async Task HandleChatDeletionOrLeaveAsync(ChatDto chatDto, int userId)
    {
        if (chatDto == null)
        {
            return;
        }

        if (chatDto.CreatedByUserId == userId)
        {
            if (!string.IsNullOrEmpty(chatDto.CreatedByUser.ConnectionId))
            {
                await _hubContext.Clients.Client(chatDto.CreatedByUser.ConnectionId).SendAsync("ChatDeleted", chatDto);
                // User is the creator, notify users and remove all from group
                foreach (var participant in chatDto.Participants)
                {
                    if (!string.IsNullOrEmpty(participant.ConnectionId))
                    {
                        await _hubContext.Groups.RemoveFromGroupAsync(participant.ConnectionId, chatDto.Id.ToString());
                        await _hubContext.Clients.Client(participant.ConnectionId).SendAsync("ChatDeleted", chatDto);
                    }
                }
            }
        }
        else
        {
            // User is just a participant, notify and remove user from group
            var participant = chatDto.Participants.FirstOrDefault(cp => cp.Id == userId);
            if (participant != null && !string.IsNullOrEmpty(participant.ConnectionId))
            {
                await _hubContext.Groups.RemoveFromGroupAsync(participant.ConnectionId, chatDto.Id.ToString());
                await _hubContext.Clients.Client(participant.ConnectionId).SendAsync("UserLeftChat", chatDto.ChatName);
            }
        }
    }


    public async Task NotifyMessageSentAsync(MessageDto messageDto)
    {
        await _hubContext.Clients.Group(messageDto.ChatId.ToString()).SendAsync("MessageReceived", messageDto);
    }
}
