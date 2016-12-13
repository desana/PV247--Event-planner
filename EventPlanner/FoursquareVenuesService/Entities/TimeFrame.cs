using System;
using System.Collections.Generic;

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
        public List<Time> Open { get; set; } = new List<Time>();

        /// <summary>
        /// Checks if <see cref="Days"/> includes <paramref name="targetDay"/>.
        /// </summary>
        /// <param name="targetDay">Day to be checked.</param>
        /// <returns><code>True</code> if <paramref name="targetDay"/> is included in <see cref="Days"/>.</returns>
        internal bool Includes(DayOfWeek targetDay)
        {
            // split by slot
            var openDays = Days.Split(',');

            foreach (var day in openDays)
            {
                // split by intervals
                var fromToRange = day.Contains("-") ? day.Split('-') : day.Split('–');
                
                // if current day is range
                if (fromToRange.Length > 1)
                {
                    var startDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayCodeToDayName(fromToRange[0]));
                    var endDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayCodeToDayName(fromToRange[1]));

                    if (startDay > endDay)
                    {
                        endDay += 7;
                    }

                    if (startDay <= targetDay && targetDay <= endDay)
                    {
                        return true;
                    }
                    else if (startDay <= targetDay + 7 && targetDay + 7 <= endDay)
                    {
                        return true;
                    }
                }
                else
                {
                    var exactDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), DayCodeToDayName(day));
                    if (targetDay.Equals(exactDay))
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        /// <summary>
        /// Converts shortened date representation to whole day name.
        /// </summary>
        /// <param name="dayCode">Shortened name.</param>
        /// <returns>Whole weekday name.</returns>
        private string DayCodeToDayName(string dayCode)
        {
            dayCode = dayCode.Replace(" ", string.Empty);

            switch (dayCode)
            {
                case "Mon":
                    return "Monday";

                case "Tue":
                    return "Tuesday";

                case "Wed":
                    return "Wednesday";

                case "Thu":
                    return "Thursday";

                case "Fri":
                    return "Friday";

                case "Sun":
                    return "Sunday";

                default:
                    return "Saturday";
            }           
        }
    }
}