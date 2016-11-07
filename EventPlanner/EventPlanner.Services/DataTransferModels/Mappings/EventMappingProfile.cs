using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Mappings
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<Entities.Event, EventTransferModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.EventLink, opt => opt.MapFrom(src => src.EventLink))
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<EventTransferModel, Entities.Event>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.EventLink, opt => opt.MapFrom(src => src.EventLink))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
