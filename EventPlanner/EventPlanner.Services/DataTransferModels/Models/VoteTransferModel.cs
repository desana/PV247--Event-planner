namespace EventPlanner.Services.DataTransferModels.Models
{
    public class VoteTransferModel
    {
        public int VoteId { get; set; }

        /// <summary>
        /// Time and place of the choice.
        /// </summary>
        public TimeAtPlaceTransferModel TimeAtPlace { get; set; }

        /// <summary>
        /// Number of votes for the choice.
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Event to which this vote belongs to.
        /// </summary>
        public EventTransferModel Event { get; set; }
    }
}