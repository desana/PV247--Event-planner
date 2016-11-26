using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mapping
{
    public class TimeAtPlaceMapping : Profile
    {
        public TimeAtPlaceMapping()
        {
            CreateMap<TimeAtPlaceTransferModel, TimeAtPlaceViewModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<TimeAtPlaceViewModel, TimeAtPlaceTransferModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
