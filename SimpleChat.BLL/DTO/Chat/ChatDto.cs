using SimpleChat.BLL.DTO.Message;
using SimpleChat.BLL.DTO.User;

namespace SimpleChat.BLL.DTO.Chats;

public class ChatDto
{
    public int Id { get; set; }
    public string ChatName { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }
    public UserDto CreatedByUser { get; set; } = new();
    public List<UserDto> Participants { get; set; } = new();
    public List<MessageDto>? Messages { get; set; }
}
