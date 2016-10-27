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
        public int EventId
        {
            get;
            set;
        }

        /// <summary>
        /// Name of the event.
        /// </summary>
        [Required]
        [StringLength(50)]
        public string EventName
        {
            get;
            set;
        }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string EventDescription
        {
            get;
            set;
        }

        /// <summary>
        /// Link to vote.
        /// </summary>
        [Required]
        public string EventLink
        {
            get;
            set;
        }

        /// <summary>
        /// Collection of all votes for this event.
        /// </summary>
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

        /// <summary>
        /// Collection of all possible places for this event.
        /// </summary>
        public ICollection<Place> Places { get; set; } = new List<Place>();

        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<TimeAtPlace> TimesAtPlaces { get; set; } = new List<TimeAtPlace>();
    }
}