using System.Data.Entity;
using EventPlanner.Entities;

namespace EventPlanner
{
    /// <summary>
    /// Database context of event planner. 
    /// </summary>
    public class EventPlannerDbContext : DbContext
    {
        /// <summary>
        /// Places used in votes.
        /// </summary>
        public DbSet<Place> Places
        {
            get;
            set;
        }


        /// <summary>
        /// Active vote choices. 
        /// </summary>
        public DbSet<Vote> Votes
        {
            get;
            set;
        }


        /// <summary>
        /// Available time slots used for current votes.
        /// </summary>
        public DbSet<TimeAtPlace> TimesAtPlaces
        {
            get;
            set;
        }
    }
}
