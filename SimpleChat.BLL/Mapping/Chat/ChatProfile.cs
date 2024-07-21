using AutoMapper;
using SimpleChat.BLL.DTO.Chats;
using SimpleChat.DAL.Models.Chats;

namespace SimpleChat.BLL.Mapping.Chats;

public class ChatProfile : Profile
{
    public ChatProfile() 
    {
        CreateMap<CreateChatDto, Chat>();

        CreateMap<Chat, ChatDto>()
            .ForMember(dest => dest.Participants,
            opt => opt.MapFrom(src => src.ChatParticipant.Select(cp => cp.User)));
    }
}
