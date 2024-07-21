using SimpleChat.DAL.Models.ChatParticipants;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces.ChatParticipants;
using SimpleChat.DAL.Repositories.Realizations.Base;

namespace SimpleChat.DAL.Repositories.Realizations.ChatParticipants;

public class ChatParticipantRepository : BaseRepository<ChatParticipant>, IChatParticipantRepository
{
    public ChatParticipantRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }

}
