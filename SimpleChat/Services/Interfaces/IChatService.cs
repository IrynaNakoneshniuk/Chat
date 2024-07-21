using SimpleChat.BLL.DTO.Chats;
using SimpleChat.BLL.DTO.Message;

namespace SimpleChat.WebApi.Services.Interfaces;

public interface IChatService
{
    Task HandleChatDeletionOrLeaveAsync(ChatDto chatDto, int userId);
    Task CreateChatInGroupAsync(ChatDto chatDto);
    Task NotifyMessageSentAsync(MessageDto messageDto);
}
