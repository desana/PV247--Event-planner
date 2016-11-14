using System.Collections.Generic;

namespace EventPlanner.Services.DataTransferModels.Models
{
    public class EventTransferModel
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link to vote.
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<TimeAtPlaceTransferModel> TimesAtPlaces { get; set; } = new List<TimeAtPlaceTransferModel>();

    }
}

