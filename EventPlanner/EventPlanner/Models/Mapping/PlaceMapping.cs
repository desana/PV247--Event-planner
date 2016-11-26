using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mapping
{
    public class PlaceMapping : Profile
    {
        public PlaceMapping()
        {
            CreateMap<PlaceTransferModel, PlaceViewModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PlaceViewModel, PlaceTransferModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
