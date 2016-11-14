using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mappings
{
    public class TimeAtPlaceMappingProfile : Profile
    {
        public TimeAtPlaceMappingProfile()
        {
            CreateMap<TimeAtPlaceViewModel, TimeAtPlaceTransferModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForAllOtherMembers(dst => dst.Ignore());
            //    .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
            //   .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time));
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event));

            CreateMap<TimeAtPlaceTransferModel, TimeAtPlaceViewModel>()
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
            //    .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
