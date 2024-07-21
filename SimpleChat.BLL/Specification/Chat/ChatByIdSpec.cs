using Ardalis.Specification;
using SimpleChat.DAL.Models.Chats;

namespace SimpleChat.BLL.Specification.Chats;

public class ChatByIdSpec : Specification<Chat>
{
    public ChatByIdSpec(int chatId)
    {
        Query.Include(chat => chat.ChatParticipant)
             .ThenInclude(cp => cp.User)
             .Where(chat => chat.Id == chatId);
    }
}
