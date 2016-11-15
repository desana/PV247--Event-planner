using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Mappings
{
    public class PlaceMappingProfile : Profile
    {
        public PlaceMappingProfile()
        {
            CreateMap<Entities.Place, PlaceTransferModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForAllOtherMembers(dst => dst.Ignore());
                
            CreateMap<PlaceTransferModel, Entities.Place>()
                .ForMember(dst => dst.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.FourSquareLink, opt => opt.MapFrom(src => src.FourSquareLink))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
