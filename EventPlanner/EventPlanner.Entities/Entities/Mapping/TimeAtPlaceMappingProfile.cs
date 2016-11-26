using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class TimeAtPlaceMappingProfile : Profile
    {
        public TimeAtPlaceMappingProfile()
        {
            CreateMap<TimeAtPlace, DTO.Event.TimeAtPlace>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time));

            CreateMap<DTO.Event.TimeAtPlace, TimeAtPlace>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.PlaceId, opt => opt.Ignore())
                .ForMember(dst => dst.Place, opt => opt.Ignore());
        }
    }
}
