using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Representation of times from opening hours of Foursquare venue (https://developer.foursquare.com/docs/responses/hours.html).
    /// </summary>
    public class Time
    {
        /// <summary>
        /// Time from opening hours.
        /// </summary>
        public string RenderedTime { get; set; }
    }
}
