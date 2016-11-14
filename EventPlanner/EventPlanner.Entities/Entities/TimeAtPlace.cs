using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Represents one time slot at specified place.  
    /// </summary>
    public class TimeAtPlace
    {
        /// <summary>
        /// Primary key of the time slot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Place at which time slot takes place on.
        /// </summary>
        public Place Place { get; set; }

        /// <summary>
        /// Starting time of the time slot.
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Number of votes for this timeslot.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Event which time slot belongs to.
        /// </summary>
        public Event Event { get; set; }
    }
}
