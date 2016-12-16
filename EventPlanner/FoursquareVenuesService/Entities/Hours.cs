using System;
using System.Collections.Generic;
using System.Linq;

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

        /// <summary>
        /// Gets open time according to <paramref name="dayOfWeek"/>.
        /// </summary>
        /// <param name="dayOfWeek">Day to find open hours.</param>
        /// <returns>Open hours.</returns>
        public IEnumerable<Time> GetOpenTimeAtDay(DayOfWeek dayOfWeek)
        {
            return Timeframes
                .FirstOrDefault(timeFrame => timeFrame
                    .Includes(dayOfWeek))
                ?.Open;
        }
    }
}