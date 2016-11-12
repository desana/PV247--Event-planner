using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Services.DataTransferModels.Mappings
{
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<Entities.Vote, VoteTransferModel>()
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                 .ForMember(dst => dst.TimeAtPlace, opt => opt.MapFrom(src => src.TimeAtPlace))
                .ForAllOtherMembers(dst => dst.Ignore());
            //      .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
                

            CreateMap<VoteTransferModel, Entities.Vote>()
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
           //     .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
          //      .ForMember(dst => dst.TimeAtPlace, opt => opt.MapFrom(src => src.TimeAtPlace))
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
