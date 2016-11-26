using System;

namespace EventPlanner.DTO.Event
{
    public class TimeAtPlace
    {
        /// <summary>
        /// Primary key of the time slot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Starting time of the time slot.
        /// </summary>
        public DateTime Time { get; set; }
    }
}