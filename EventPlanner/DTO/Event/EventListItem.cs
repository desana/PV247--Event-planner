using System;

namespace EventPlanner.DTO.Event
{
    public class EventListItem
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Description { get; set; }
    }
}
