using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    /// <summary>
    /// Represents one time slot at specified place.  
    /// </summary>
    internal class TimeAtPlace
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
    }
}
