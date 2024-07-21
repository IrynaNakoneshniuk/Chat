using Ardalis.Specification;
using SimpleChat.DAL.Models.Chats;

namespace SimpleChat.BLL.Specification.Chats;

public class ChatsByUserIdSpec : Specification<Chat>
{
    public ChatsByUserIdSpec(int userId)
    {
        Query.Include(chat => chat.ChatParticipant)
             .ThenInclude(cp => cp.User)
             .Include(chat => chat.Messages)
             .Where(chat => chat.ChatParticipant.Any(cp => cp.UserId == userId) || chat.CreatedByUserId == userId);
    }
}
