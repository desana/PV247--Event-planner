﻿using System;
using System.Collections.Generic;

namespace EventPlanner.Services.DataTransferModels.Models
{
    public class TimeAtPlaceTransferModel
    {
        /// <summary>
        /// Primary key of the time slot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Place at which time slot takes place on.
        /// </summary>
        public PlaceTransferModel Place { get; set; }

        /// <summary>
        /// Starting time of the time slot.
        /// </summary>
        public List<DateTime> Time { get; set; }

        /// <summary>
        /// Event to which this belongs to.
        /// </summary>
        public EventTransferModel Event { get; set; }

        /// <summary>
        /// Number of recorded votes.
        /// </summary>
        public int Votes { get; set; }
    }
}