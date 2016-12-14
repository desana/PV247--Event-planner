using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

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
            var openingHoursRange = GetOpeningHoursRange().ToArray();

            var start = openingHoursRange[0].TimeOfDay;
            var end = openingHoursRange[1].TimeOfDay;

            if (start <= end)
            {
                return time >= start && time <= end;
            }

            return time >= start || time <= end;
        }
        
        /// <summary>
        /// Returns time range in invariant culture.
        /// </summary>
        /// <returns>Opening hours in invarian culture.</returns>
        public string ReturnAsInvariantCulture()
        {
            var openingHoursRange = GetOpeningHoursRange().ToArray();

            var start = openingHoursRange[0]
                .ToString("HH:mm");
            var end = openingHoursRange[1]
                .ToString("HH:mm");

            return $"{start}-{end}";
        }

        /// <summary>
        /// Returns range of opening hours.
        /// </summary>
        /// <returns>Collection of size 2.</returns>
        private IEnumerable<DateTime> GetOpeningHoursRange()
        {
            var range = RenderedTime.Contains("-") ? RenderedTime.Split('-') : RenderedTime.Split('–');

            foreach (var word in range)
            {
                yield return ReplaceStringTime(word);
            }
        }

        /// <summary>
        /// Replaces word time representation for <see cref="DateTime"/> representation.
        /// </summary>
        /// <param name="word">Input word.</param>
        /// <returns>Correct <see cref="DateTime"/>.</returns>
        private DateTime ReplaceStringTime(string word)
        {
            word = Regex.Replace(word, "noon", "12:00", RegexOptions.IgnoreCase);
            word = Regex.Replace(word, "midnight", "00:00", RegexOptions.IgnoreCase);

            return Convert
                .ToDateTime(word, CultureInfo.InvariantCulture);
        }
    }
}
