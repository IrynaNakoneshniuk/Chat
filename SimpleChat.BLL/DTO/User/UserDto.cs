using SimpleChat.DAL.Models.ChatParticipants;

namespace SimpleChat.BLL.DTO.User;

public class UserDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? ConnectionId { get; set; } = string.Empty;
}
