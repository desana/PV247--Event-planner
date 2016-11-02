namespace EventPlanner.Entities
{
    /// <summary>
    /// Represents one choice in vote.
    /// </summary>
    public class Vote
    {
        /// <summary>
        /// Primary key of the vote choice.
        /// </summary>
        public int VoteId
        {
            get;
            set;
        }

        /// <summary>
        /// Time and place of the choice.
        /// </summary>
        public TimeAtPlace TimeAtPlace
        {
            get;
            set;
        }


        /// <summary>
        /// Number of votes for the choice.
        /// </summary>
        public int Votes
        {
            get;
            set;
        }

        /// <summary>
        /// Event to which this vote belongs to.
        /// </summary>
        public Event Event
        {
            get;
            set;
        }

    }
}
