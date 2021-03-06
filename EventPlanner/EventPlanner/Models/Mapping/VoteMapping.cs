﻿using AutoMapper;
using EventPlanner.DTO.Event;
using EventPlanner.Models.Vote;

namespace EventPlanner.Models.Mapping
{
    public class VoteMapping : Profile
    {
        public VoteMapping()
        {
            CreateMap<DTO.Event.Event, VoteViewModel>()
                .ForMember(dst => dst.EventId, opt => opt.MapFrom(src => src.EventId))
                .ForMember(dst => dst.EventName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dst => dst.Places, opt => opt.MapFrom(src => src.Places))
                .ForMember(dst => dst.VoteSession, opt => opt.Ignore());

            CreateMap<Place, VotePlaceViewModel>()
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src))
                .ForMember(dst => dst.PlacePhotoUrl, opt => opt.Ignore())
                .ForMember(dst => dst.Location, opt => opt.Ignore());
        }
    }
}
