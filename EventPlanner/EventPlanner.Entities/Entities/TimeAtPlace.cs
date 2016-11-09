using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public int TimeAtPlaceId { get; set; }

        /// <summary>
        /// Place at which time slot takes place on.
        /// </summary>
        public Place Place { get; set; }

        /// <summary>
        /// Starting time of the time slot.
        /// </summary>
        public ICollection<DateTime> Time { get; set; } = new List<DateTime>();

        /// <summary>
        /// Event to which this belongs to.
        /// </summary>
        public Event Event { get; set; }
    }
}
