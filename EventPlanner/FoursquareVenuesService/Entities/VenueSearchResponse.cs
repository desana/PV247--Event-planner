﻿using System.Collections.Generic;

namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Data from response from venues/search.
    /// </summary>
    public class VenueSearchResponse
    {
        /// <summary>
        /// Final list witch venues.
        /// </summary>
        public List<Venue> Venues { get; set; }
    }
}