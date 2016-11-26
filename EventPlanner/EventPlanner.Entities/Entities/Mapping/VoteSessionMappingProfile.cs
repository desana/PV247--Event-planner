using AutoMapper;

namespace EventPlanner.Entities.Entities.Mapping
{
    public class VoteSessionMappingProfile : Profile
    {
        public VoteSessionMappingProfile()
        {
            CreateMap<VoteSession, DTO.Vote.VoteSession>()
                .ForMember(dst => dst.VoteSessionId, opt => opt.MapFrom(src => src.VoteSessionId))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.VoterName, opt => opt.MapFrom(src => src.VoterName))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dst => dst.LastModified, opt => opt.MapFrom(src => src.LastModified));

            CreateMap<DTO.Vote.VoteSession, VoteSession>()
                .ForMember(dst => dst.VoteSessionId, opt => opt.MapFrom(src => src.VoteSessionId))
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.VoterName, opt => opt.MapFrom(src => src.VoterName))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                .ForMember(dst => dst.LastModified, opt => opt.MapFrom(src => src.LastModified))
                .ForMember(dst => dst.Event, opt => opt.Ignore());
        }
    }
}
