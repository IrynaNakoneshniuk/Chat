using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.Specification.Users;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Interfaces.Chats;
using SimpleChat.WebApi.Hubs;
using SimpleChat.WebApi.Services.Interfaces;

namespace SimpleChat.WebApi.Services.Realizations;

public class ChatService : IChatService
{
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly IUserRepository _userRepository;
    private readonly IChatRepository _chatRepository;

    public ChatService(IHubContext<ChatHub> hubContext, IUserRepository userRepository, IMapper mapper, IChatRepository chatRepository)
    {
        _hubContext = hubContext;
        _userRepository = userRepository;
        _chatRepository = chatRepository;
    }

    public async Task CreateChatInGroupAsync(ChatDto chatDto)
    {
        foreach (var participant in chatDto.Participants)
        {
            var user = await _userRepository.GetFirstOrDefaulAsync(new FindUserByIdSpec(participant.Id));
            if (user != null && !string.IsNullOrEmpty(user.ConnectionId))
            {
                await _hubContext.Groups.AddToGroupAsync(user.ConnectionId, chatDto.ChatName);
            }
        }

        await _hubContext.Clients.Group(chatDto.ChatName).SendAsync("ChatСreated", chatDto);
    }

    public async Task HandleChatDeletionOrLeaveAsync(ChatDto chatDto, int userId)
    {
        if (chatDto == null)
        {
            return;
        }

        if (chatDto.CreatedByUserId == userId)
        {
            // User is the creator, notify users and remove all from group
            foreach (var participant in chatDto.Participants)
            {
                if (!string.IsNullOrEmpty(participant.ConnectionId))
                {
                    await _hubContext.Groups.RemoveFromGroupAsync(participant.ConnectionId, chatDto.ChatName);
                    await _hubContext.Clients.Client(participant.ConnectionId).SendAsync("ChatDeleted", chatDto.ChatName);
                }
            }
        }
        else
        {
            // User is just a participant, notify and remove user from group
            var participant = chatDto.Participants.FirstOrDefault(cp => cp.Id == userId);
            if (participant != null && !string.IsNullOrEmpty(participant.ConnectionId))
            {
                await _hubContext.Groups.RemoveFromGroupAsync(participant.ConnectionId, chatDto.ChatName);
                await _hubContext.Clients.Client(participant.ConnectionId).SendAsync("UserLeftChat", chatDto.ChatName);
            }
        }
    }


    public Task SendMessageToGroupAsync(string groupName, string message)
    {
        throw new NotImplementedException();
    }
}
