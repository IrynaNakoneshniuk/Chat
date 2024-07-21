using Ardalis.Specification;
using SimpleChat.DAL.Models.Users;
using SimpleChat.DAL.Repositories.Interfaces.Base;

namespace SimpleChat.DAL.Repositories.Interfaces;

public interface IUserRepository : IBaseRepository<User>
{
}
