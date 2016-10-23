﻿using System.Collections.Generic;

namespace EventPlanner.Entities
{
    /// <summary>
    /// Represents one event.
    /// </summary>
    public class PlannedEvent
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
        /// Collection of all votes for this event.
        /// </summary>
        public ICollection<Vote> Votes { get; set; } = new List<Vote>();

        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<TimeAtPlace> TimesAtPlaces { get; set; } = new List<TimeAtPlace>();
    }
}