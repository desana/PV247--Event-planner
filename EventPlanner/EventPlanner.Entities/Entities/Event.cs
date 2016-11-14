using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Represents one event.
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link to vote.
        /// </summary>
        [Required]
        public string Link { get; set; }

        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<TimeAtPlace> TimesAtPlaces { get; set; } = new List<TimeAtPlace>();
    }
}