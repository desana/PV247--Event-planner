// <auto-generated />
namespace EventPlanner.Entities.Migrations
{
    using System.CodeDom.Compiler;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Resources;
    
    [GeneratedCode("EntityFramework.Migrations", "6.1.3-40302")]
    public sealed partial class EventIdToGuid : IMigrationMetadata
    {
        private readonly ResourceManager Resources = new ResourceManager(typeof(EventIdToGuid));
        
        string IMigrationMetadata.Id
        {
            get { return "201612062226046_EventIdToGuid"; }
        }
        
        string IMigrationMetadata.Source
        {
            get { return null; }
        }
        
        string IMigrationMetadata.Target
        {
            get { return Resources.GetString("Target"); }
        }
    }
}