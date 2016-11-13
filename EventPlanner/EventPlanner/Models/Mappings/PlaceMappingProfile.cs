using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mappings
{
    public class PlaceMappingProfile : Profile
    {
        public PlaceMappingProfile()
        {
            CreateMap<PlaceViewModel, PlaceTransferModel>()
                .ForMember(dst => dst.PlaceId, opt => opt.MapFrom(src => src.PlaceId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForAllOtherMembers(dst => dst.Ignore());
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event));

            CreateMap<PlaceTransferModel, PlaceViewModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
            //    .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
