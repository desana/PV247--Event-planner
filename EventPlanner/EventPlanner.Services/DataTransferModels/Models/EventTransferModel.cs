using System.Collections.Generic;

namespace EventPlanner.Services.DataTransferModels.Models
{
    public class EventTransferModel
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Link to vote.
        /// </summary>
        public string EventLink { get; set; }

        /// <summary>
        /// Collection of all votes for this event.
        /// </summary>
        public ICollection<VoteTransferModel> Votes { get; set; } = new List<VoteTransferModel>();

        /// <summary>
        /// Collection of all possible places for this event.
        /// </summary>
        public ICollection<PlaceTransferModel> Places { get; set; } = new List<PlaceTransferModel>();

        /// <summary>
        /// Collection of all possible times at concrete places for this event.
        /// </summary>
        public ICollection<TimeAtPlaceTransferModel> TimesAtPlaces { get; set; } = new List<TimeAtPlaceTransferModel>();

    }
}

