using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventPlanner.Entities.Entities
{
    /// <summary>
    /// Represents FourSquare location.
    /// </summary>
    internal class Place
    {
        /// <summary>
        /// Primary key of place.
        /// </summary>
        public int Id { get; set; }

        [ForeignKey("Event")]
        public int EventId { get; set; }

        public virtual Event Event { get; set; }

        /// <summary>
        /// Display name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the FourSquare location.
        /// </summary>
        public string FourSquareId { get; set; }

        /// <summary>
        /// Link to FourSquare location.
        /// </summary>
        public string FourSquareLink { get; set; }

        /// <summary>
        /// Collection of all possible times.
        /// </summary>
        public ICollection<TimeAtPlace> Times { get; set; } = new List<TimeAtPlace>();
    }
}
