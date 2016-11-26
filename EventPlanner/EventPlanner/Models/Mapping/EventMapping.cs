using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mapping
{
    public class EventMapping : Profile
    {
        public EventMapping()
        {
            CreateMap<EventTransferModel, EventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.TimesAtPlaces, opt => opt.MapFrom(src => src.TimesAtPlaces))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<EventViewModel, EventTransferModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.Description, opt => opt.MapFrom(src => src.EventDescription))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.TimesAtPlaces, opt => opt.MapFrom(src => src.TimesAtPlaces))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
