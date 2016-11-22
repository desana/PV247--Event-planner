using System.Data.Entity;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Database context of event planner. 
    /// </summary>
    public class EventPlannerContext : DbContext
    {
        public EventPlannerContext() : base("EventPlannerContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EventPlannerContext, Migrations.Configuration>());
        }

        public EventPlannerContext(string connectionString)
            : base(connectionString)
        {
        }

        /// <summary>
        /// Places used in votes.
        /// </summary>
        public virtual DbSet<Place> Places { get; set; }
        
        /// <summary>
        /// Available time slots used for current votes.
        /// </summary>
        public virtual DbSet<TimeAtPlace> TimesAtPlaces { get; set; }

        /// <summary>
        /// Active events.
        /// </summary>
        public virtual DbSet<Event> Events { get; set; }
    }
}
