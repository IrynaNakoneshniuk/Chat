using SimpleChat.DAL.Models.ChatParticipants;

namespace SimpleChat.DAL.Models.Users;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string ? ConnectionId { get; set; } = string.Empty;
    public ICollection<ChatParticipant> ? ChatParticipant { get; set; }
}
