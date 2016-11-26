using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Entities.Entities
{
    /// <summary>
    /// Represents one event.
    /// </summary>
    internal class Event
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
        /// Collection of all possible locations and times.
        /// </summary>
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}