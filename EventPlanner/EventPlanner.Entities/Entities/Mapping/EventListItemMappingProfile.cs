using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class EventListItemMappingProfile : Profile
    {
        public EventListItemMappingProfile()
        {
            CreateMap<Event, DTO.Event.EventListItem>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Link, opt => opt.MapFrom(src => src.Link));
        }
    }
}
