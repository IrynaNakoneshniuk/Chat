namespace SimpleChat.BLL.DTO.Chats;

public class CreateChatDto
{
    public string ChatName { get; set; } = string.Empty;
    public int CreatedByUserId { get; set; }
    public List<int> ParticipantIds { get; set; } = new List<int>();
}
