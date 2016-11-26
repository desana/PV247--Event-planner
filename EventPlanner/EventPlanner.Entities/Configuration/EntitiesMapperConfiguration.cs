using AutoMapper;
using EventPlanner.Entities.Entities.Mapping;

namespace EventPlanner.Entities.Configuration
{
    public static class EntitiesMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<EventListItemMappingProfile>();
            configuration.AddProfile<EventMappingProfile>();
            configuration.AddProfile<PlaceMappingProfile>();
            configuration.AddProfile<TimeAtPlaceMappingProfile>();
            configuration.AddProfile<VoteSessionMappingProfile>();
            configuration.AddProfile<VoteMappingProfile>();
        }
    }
}
