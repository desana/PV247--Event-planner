using AutoMapper;
using EventPlanner.Services.DataTransferModels.Mapping;

namespace EventPlanner.Services.Configuration
{
    public static class ServicesMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression configuration)
        {
            configuration.AddProfile<EventMappingProfile>();
        }
    }
}
