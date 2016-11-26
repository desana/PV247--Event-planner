using System;
using AutoMapper;
using EventPlanner.DTO.Vote;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class VoteMappingProfile : Profile
    {
        public VoteMappingProfile()
        {
            CreateMap<Vote, DTO.Vote.Vote>()
                .ForMember(dst => dst.VoteId, opt => opt.MapFrom(src => src.VoteId))
                .ForMember(dst => dst.TimeAtPlaceId, opt => opt.MapFrom(src => src.TimeAtPlaceId))
                .ForMember(dst => dst.Value, opt => opt.ResolveUsing(src => Enum.Parse(typeof(VoteValueEnum), src.Value, true)));

            CreateMap<DTO.Vote.Vote, Vote>()
                .ForMember(dst => dst.VoteId, opt => opt.MapFrom(src => src.VoteId))
                .ForMember(dst => dst.TimeAtPlaceId, opt => opt.MapFrom(src => src.TimeAtPlaceId))
                .ForMember(dst => dst.Value, opt => opt.ResolveUsing(src => src.Value.ToString()))
                .ForMember(dst => dst.TimeAtPlace, opt => opt.Ignore());
        }
    }
}
