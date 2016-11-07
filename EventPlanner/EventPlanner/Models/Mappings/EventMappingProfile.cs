using System.Collections;
using System.Collections.Generic;
using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mappings
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<EventTransferModel, EventViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForAllOtherMembers(dst => dst.Ignore()); 

            CreateMap<EventViewModel, EventTransferModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.EventName))
                .ForMember(dst => dst.EventDescription, opt => opt.MapFrom(src => src.EventDescription))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
