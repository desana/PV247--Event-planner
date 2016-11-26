using System.Collections.Generic;

namespace EventPlanner.DTO.Event
{
    public class Event : EventListItem
    {
        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<Place> Places { get; set; } = new List<Place>();
    }
}

