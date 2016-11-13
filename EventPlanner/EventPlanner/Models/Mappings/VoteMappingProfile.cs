using System.Collections.Generic;
using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mappings
{
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<VoteViewModel, VoteTransferModel>()
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                 .ForAllOtherMembers(dst => dst.Ignore());
            //  .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
            //  .ForMember(dst => dst.TimeAtPlace, opt => opt.MapFrom(src => src.TimeAtPlace));
            .ForAllOtherMembers(dst => dst.Ignore());


            CreateMap<VoteTransferModel, VoteViewModel>()
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
             //   .ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event))
                .ForMember(dst => dst.TimeAtPlace, opt => opt.MapFrom(src => src.TimeAtPlace))
                .ForAllOtherMembers(dst => dst.Ignore());

        }
    }
}
