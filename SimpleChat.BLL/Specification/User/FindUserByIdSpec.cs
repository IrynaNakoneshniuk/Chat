using Ardalis.Specification;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.BLL.Specification.Users;

public class FindUserByIdSpec : Specification<User>
{
    public FindUserByIdSpec(int userId)
    {
        Query.Where(u => u.Id == userId);
    }
}
