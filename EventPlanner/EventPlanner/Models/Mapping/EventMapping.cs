using AutoMapper;
using EventPlanner.Models.Event;

namespace EventPlanner.Models.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            // CreateEventViewModel mappings

            CreateMap<DTO.Event.Event, CreateEventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateEventViewModel, DTO.Event.Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription))
                .ForAllOtherMembers(opt => opt.Ignore()); 

            // AddPlacesViewModel mappings

            CreateMap<DTO.Event.Event, AddPlacesViewModel>()
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AddPlacesViewModel, DTO.Event.Event>()
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForAllOtherMembers(opt => opt.Ignore());

            // EventViewModel mappings

            CreateMap<DTO.Event.Event, EventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EventViewModel, DTO.Event.Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
