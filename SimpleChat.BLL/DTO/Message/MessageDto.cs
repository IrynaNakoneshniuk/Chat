using SimpleChat.DAL.Models.Chats;

namespace SimpleChat.BLL.DTO.Message;

public class MessageDto
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public int UserId { get; set; }
    public int ChatId { get; set; }
}
