using System;
using System.ComponentModel.DataAnnotations.Schema;

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
        public int PlaceId { get; set; }

        /// <summary>
        /// Place at which time slot takes place on.
        /// </summary>
        [ForeignKey("PlaceId")]

        public virtual Place Place { get; set; }

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
        public int EventId { get; set; }

        /// <summary>
        /// Event which time slot belongs to.
        /// </summary>
        [ForeignKey("EventId")]
        public virtual Event Event { get; set; }
    }
}
