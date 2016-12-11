using AutoMapper;
using EventPlanner.DTO.Event;
using EventPlanner.Models.Event;

namespace EventPlanner.Models.Mapping
{
    public class PlaceMapping : Profile
    {
        public PlaceMapping()
        {
            CreateMap<Place, PlaceViewModel>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.PlaceId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Times, opt => opt.MapFrom(src => src.Times))
                .ForAllOtherMembers(opt => opt.Ignore());

            CreateMap<PlaceViewModel, Place>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.PlaceId))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Times, opt => opt.MapFrom(src => src.Times))
                .ForAllOtherMembers(opt => opt.Ignore());
        }
    }
}
