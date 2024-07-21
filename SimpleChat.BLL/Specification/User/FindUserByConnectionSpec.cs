using Ardalis.Specification;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.BLL.Specification.Users;

public class FindUserByConnectionSpec : Specification<User>
{
    public FindUserByConnectionSpec(string connectionId)
    {
        Query.Where(u => u.ConnectionId == connectionId);
    }
}
