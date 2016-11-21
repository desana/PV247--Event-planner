using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Mappings
{
    public class TimeAtPlaceMappingProfile : Profile
    {
        public TimeAtPlaceMappingProfile()
        {
            CreateMap<Entities.TimeAtPlace, TimeAtPlaceTransferModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                //.ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event)) // This causes stack overflow.
                .ForAllOtherMembers(dst => dst.Ignore());
                
            CreateMap<TimeAtPlaceTransferModel, Entities.TimeAtPlace>()
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                //.ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event)) // This causes stack overflow.
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
