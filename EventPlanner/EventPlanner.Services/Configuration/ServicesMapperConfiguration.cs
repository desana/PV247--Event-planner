using AutoMapper;
using EventPlanner.Services.DataTransferModels.Mappings;

namespace EventPlanner.Services.Configuration
{
    public static class ServicesMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<EventMappingProfile>();
            configuration.AddProfile<PlaceMappingProfile>();
            configuration.AddProfile<TimeAtPlaceMappingProfile>();
            configuration.AddProfile<VoteMappingProfile>();
        }
    }
}
