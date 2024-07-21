using AutoMapper;
using SimpleChat.BLL.DTO.Message;
using SimpleChat.DAL.Models.Messages;

namespace SimpleChat.BLL.Mapping.Messages;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<MessageDto, Message>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.ChatId, opt => opt.MapFrom(src => src.ChatId))
            .ReverseMap(); 
    }
}