using System.Collections.Generic;

namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Representation of opening hours of Foursquare venue (https://developer.foursquare.com/docs/responses/hours.html).
    /// </summary>
    public class Hours
    {
        /// <summary>
        /// Opening hours for a set of days
        /// </summary>
        public List<Timeframe> Timeframes { get; set; }
    }
}