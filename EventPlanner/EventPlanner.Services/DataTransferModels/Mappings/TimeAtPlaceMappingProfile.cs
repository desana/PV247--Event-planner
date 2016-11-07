using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Mappings
{
    public class TimeAtPlaceMappingProfile : Profile
    {
        public TimeAtPlaceMappingProfile()
        {
            CreateMap<Entities.TimeAtPlace, TimeAtPlaceTransferModel>()
                .ForMember(dst => dst.TimeAtPlaceId, opt => opt.MapFrom(src => src.TimeAtPlaceId))
                //   .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time));
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event));

            CreateMap<TimeAtPlaceTransferModel, Entities.TimeAtPlace>()
             //   .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
