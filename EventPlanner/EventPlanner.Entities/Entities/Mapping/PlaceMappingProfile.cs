using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class PlaceMappingProfile : Profile
    {
        public PlaceMappingProfile()
        {
            CreateMap<Place, DTO.Event.Place>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Times, opt => opt.MapFrom(src => src.Times));

            CreateMap<DTO.Event.Place, Place>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))                
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForMember(dst => dst.FourSquareId, opt => opt.MapFrom(src => src.FourSquareId))
                .ForMember(dst => dst.Times, opt => opt.MapFrom(src => src.Times))
                .ForMember(dst => dst.Event, opt => opt.Ignore())
                .ForMember(dst => dst.EventId, opt => opt.Ignore());
        }
    }
}
