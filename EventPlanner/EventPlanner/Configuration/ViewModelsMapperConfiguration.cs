using AutoMapper;
using EventPlanner.Models.Mapping;

namespace EventPlanner.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<EventMapping>();
            cfg.AddProfile<PlaceMapping>();
            cfg.AddProfile<TimeAtPlaceMapping>();
            cfg.AddProfile<VoteMapping>();
        }
    }
}
