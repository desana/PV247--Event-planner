using System;
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
        public int TimeAtPlaceId
        {
            get;
            set;
        }


        /// <summary>
        /// Place at which time slot takes place on.
        /// </summary>
        public Place Place
        {
            get;
            set;
        }


        /// <summary>
        /// Starting time of the time slot.
        /// </summary>
        [Required]
        public DateTime Time
        {
            get;
            set;
        }

        /// <summary>
        /// Event to which this belongs to.
        /// </summary>
        public Event Event
        {
            get;
            set;
        }
    }
}
