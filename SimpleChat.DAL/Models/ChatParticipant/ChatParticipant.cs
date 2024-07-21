using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.DAL.Models.ChatParticipants;

public class ChatParticipant
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public Chat Chat { get; set; } = new();
    public int UserId { get; set; }
    public User User { get; set; } = new();
}