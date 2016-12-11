using System;
using System.Globalization;

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
        /// Checks if <paramref name="time"/> is in range of opening hours.
        /// </summary>
        /// <param name="time">Time to check.</param>
        /// <returns>True if venue is open at given time.</returns>
        public bool IsOpenAtTime(TimeSpan time)
        {
            var openingHoursRange = RenderedTime.Contains("-") ? RenderedTime.Split('-') : RenderedTime.Split('–');
            
            var start = Convert
                .ToDateTime(openingHoursRange[0], CultureInfo.InvariantCulture)
                .TimeOfDay;

            var end = Convert
                .ToDateTime(openingHoursRange[1], CultureInfo.InvariantCulture)
                .TimeOfDay;
            
            if (start <= end)
            {
                return time >= start && time <= end;
            }
            else
            {
                return time >= start || time <= end;
            }
        }
    }
}
