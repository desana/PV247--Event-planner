using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class EventListItemMappingProfile : Profile
    {
        public EventListItemMappingProfile()
        {
            CreateMap<Event, DTO.Event.EventListItem>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
