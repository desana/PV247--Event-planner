using AutoMapper;

namespace EventPlanner.Services.DataTransferModels.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Entities.Event, Event>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.EventLink, opt => opt.MapFrom(src => src.EventLink))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<Event, Entities.Event>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.EventLink, opt => opt.MapFrom(src => src.EventLink))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
