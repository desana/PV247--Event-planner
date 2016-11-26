using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Event, DTO.Event.Event>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places));

            CreateMap<DTO.Event.Event, Event>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.Link, opt => opt.MapFrom(src => src.Link))
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places));
        }
    }
}
