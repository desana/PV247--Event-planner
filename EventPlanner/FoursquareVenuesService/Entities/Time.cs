using System;
using System.Collections.Generic;
using System.Globalization;
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

        /// <summary>
        /// Checks if <paramref name="time"/> is in range of <paramref name="openingHours"/>.
        /// </summary>
        /// <param name="time">Time to check.</param>
        /// <param name="openingHours">Venue opening hours.</param>
        /// <returns>True if venue is open at given time.</returns>
        public bool IsOpenAtTime(TimeSpan time, string openingHours)
        {
            var openingHoursRange = openingHours.Split('–');

            var start = Convert
                .ToDateTime(openingHoursRange[0], CultureInfo.InvariantCulture)
                .TimeOfDay;

            var end = Convert
                .ToDateTime(openingHoursRange[1], CultureInfo.InvariantCulture)
                .TimeOfDay;

            return start < time && time < end;
        }
    }
}
