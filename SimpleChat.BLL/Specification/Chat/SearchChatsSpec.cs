using Ardalis.Specification;
using SimpleChat.DAL.Models.Chats;

namespace SimpleChat.BLL.Specification.Chats;

public class SearchChatsSpec : Specification<Chat>
{
    public SearchChatsSpec(string searchTerm)
    {
        Query.Include(chat => chat.ChatParticipant)
             .ThenInclude(cp => cp.User)
             .Include(chat => chat.Messages)
             .Where(chat => chat.ChatName.Contains(searchTerm) ||
                            chat.ChatParticipant.Any(cp => cp.User.UserName.Contains(searchTerm)));
    }
}