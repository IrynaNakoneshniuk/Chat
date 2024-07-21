using AutoMapper;
using SimpleChat.BLL.DTO.User;
using SimpleChat.DAL.Models.Users;

namespace SimpleChat.BLL.Mapping.Users;
public class UserProfile : Profile
{
    public UserProfile() { 
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<CreateUserDto, User>().ReverseMap(); 
    }
}
