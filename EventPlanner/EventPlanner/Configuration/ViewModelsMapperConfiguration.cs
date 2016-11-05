using AutoMapper;
using EventPlanner.Models.Mappings;

namespace EventPlanner.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<EventMappingProfile>();
        }
    }
}
