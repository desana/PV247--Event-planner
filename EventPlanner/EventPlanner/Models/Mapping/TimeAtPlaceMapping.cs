using AutoMapper;
using EventPlanner.DTO.Event;
using EventPlanner.Models.Event;

namespace EventPlanner.Models.Mapping
{
    public class TimeAtPlaceMapping : Profile
    {
        public TimeAtPlaceMapping()
        {
            CreateMap<TimeAtPlace, TimeAtPlaceViewModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<TimeAtPlaceViewModel, TimeAtPlace>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
