using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.DAL.Models.Messages;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public int UserId { get; set; }
    public User User { get; set; } = new();
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = new();
}