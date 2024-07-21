using SimpleChat.DAL.Models.Messages;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces.Messages;
using SimpleChat.DAL.Repositories.Realizations.Base;

namespace SimpleChat.DAL.Repositories.Realizations.Messages;

public class MessageRepository : BaseRepository<Message>, IMessageRepository
{
    public MessageRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }
}
