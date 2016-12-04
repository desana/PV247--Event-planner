using AutoMapper;
using EventPlanner.DTO.Event;

namespace EventPlanner.Models.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            // CreateEventViewModel mappings

            CreateMap<Event, CreateEventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.Description))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<CreateEventViewModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription))
                .ForAllOtherMembers(opt => opt.Ignore()); 

            // AddPlacesViewModel mappings

            CreateMap<Event, AddPlacesViewModel>()
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<AddPlacesViewModel, Event>()
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.EventId))
                .ForAllOtherMembers(opt => opt.Ignore());

            // EventViewModel mappings

            CreateMap<Event, EventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.EventLink, opt => opt.MapFrom(src => src.Link))
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EventViewModel, Event>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.Link, opt => opt.MapFrom(src => src.EventLink))
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
