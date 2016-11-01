using AutoMapper;

namespace EventPlanner.Services.DataTransferModels.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Entities.Event, Event>()
                .ForMember(dst => dst.Title, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription));
        }
    }
}
