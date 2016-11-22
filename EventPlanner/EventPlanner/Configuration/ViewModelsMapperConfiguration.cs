using AutoMapper;
using EventPlanner.Models.Mapping;
using EventPlanner.Services.DataTransferModels.Mappings;

namespace EventPlanner.Configuration
{
    public class ViewModelsMapperConfiguration
    {
        public static void InitialializeMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AddProfile<EventMappingProfile>();
            cfg.AddProfile<PlaceMappingProfile>();
            cfg.AddProfile<TimeAtPlaceMappingProfile>();
            cfg.AddProfile<VoteMapping>();
        }
    }
}
