using SimpleChat.DAL.Models.ChatParticipants;
using SimpleChat.DAL.Models.Messages;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.DAL.Models.Chats;
public class Chat
{
    public int Id { get; set; }
    public string ChatName { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }
    public User ? CreatedByUser { get; set; } = new();
    public ICollection<ChatParticipant> ? ChatParticipant { get; set; } = new List<ChatParticipant>();
    public ICollection<Message> ? Messages { get; set; }
}