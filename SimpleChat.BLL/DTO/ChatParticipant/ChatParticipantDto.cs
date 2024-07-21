namespace SimpleChat.BLL.DTO.ChatParticipant;

public class ChatParticipantDto
{
    public int UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public int ChatId { get; set; }
    public string ChatName { get; set; } = string.Empty;
}
