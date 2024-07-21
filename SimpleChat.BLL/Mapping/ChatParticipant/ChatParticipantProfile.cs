using AutoMapper;
using SimpleChat.BLL.DTO.ChatParticipant;
using SimpleChat.DAL.Models.ChatParticipants;

namespace SimpleChat.BLL.Mapping.ChatParticipants;

public class ChatParticipantProfile : Profile
{
    public ChatParticipantProfile()
    {
        CreateMap<ChatParticipant, ChatParticipantDto>()
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.Chat.Id))
            .ForMember(dest => dest.ChatName, opt => opt.MapFrom(src => src.Chat.ChatName))
            .ReverseMap();
    }
}
