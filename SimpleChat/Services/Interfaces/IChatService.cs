﻿using SimpleChat.BLL.DTO.Chats;

namespace SimpleChat.WebApi.Services.Interfaces;

public interface IChatService
{
    Task SendMessageToGroupAsync(string groupName, string message);
    Task HandleChatDeletionOrLeaveAsync(ChatDto chatDto, int userId);
    Task CreateChatInGroupAsync(ChatDto chatDto);
}
