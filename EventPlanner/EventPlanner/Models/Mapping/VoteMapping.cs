using AutoMapper;
using EventPlanner.Models.Vote;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mapping
{
    public class VoteMapping : Profile
    {
        public VoteMapping()
        {
            CreateMap<EventTransferModel, VoteCollectionViewModel>()
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.VoteItems, opt => opt.MapFrom(src => src.TimesAtPlaces))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<TimeAtPlaceTransferModel, VoteItemViewModel>()
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.PlacePhotoUrl, opt => opt.Ignore())
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
