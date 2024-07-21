using SimpleChat.DAL.Models.Chats;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces.Chats;
using SimpleChat.DAL.Repositories.Realizations.Base;

namespace SimpleChat.DAL.Repositories.Realizations.Chats;

public class ChatRepository : BaseRepository<Chat>, IChatRepository
{
    public ChatRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }
}
