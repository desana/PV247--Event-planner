﻿using AutoMapper;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Mappings
{
    public class TimeAtPlaceMappingProfile : Profile
    {
        public TimeAtPlaceMappingProfile()
        {
            CreateMap<TimeAtPlaceViewModel, TimeAtPlaceTransferModel>()
                .ForMember(dst => dst.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                //.ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event)) // This causes stack overflow.        
                .ForAllOtherMembers(dst => dst.Ignore());

            CreateMap<TimeAtPlaceTransferModel, TimeAtPlaceViewModel>()
                .ForMember(dst => dst.Place, opt => opt.MapFrom(src => src.Place))
                .ForMember(dst => dst.Time, opt => opt.MapFrom(src => src.Time))
                .ForMember(dst => dst.Votes, opt => opt.MapFrom(src => src.Votes))
                //.ForMember(dst => dst.Event, opt => opt.MapFrom(src => src.Event)) // This causes stack overflow.
                .ForAllOtherMembers(dst => dst.Ignore());
        }
    }
}
