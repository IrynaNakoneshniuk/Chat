using Ardalis.Specification.EntityFrameworkCore;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Persistence;
using SimpleChat.DAL.Repositories.Interfaces;
using SimpleChat.DAL.Repositories.Realizations.Base;

namespace SimpleChat.DAL.Repositories.Realizations.Users;

public class UserRepository : BaseRepository<User> , IUserRepository
{
    public UserRepository(ChatDbContext dbContext) : base(dbContext)
    {
    }

}
