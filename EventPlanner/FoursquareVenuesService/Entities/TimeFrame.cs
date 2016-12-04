using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Representation of timeframes from opening hours of Foursquare venue (https://developer.foursquare.com/docs/responses/hours.html).
    /// </summary>
    public class Timeframe
    {
        /// <summary>
        /// Localized list of day names.
        /// </summary>
        public string Days { get; set; }

        /// <summary>
        /// An array of times the venue is open on days within the timeframe.
        /// </summary>
        public List<Time> Open { get; set; }
    }
}
