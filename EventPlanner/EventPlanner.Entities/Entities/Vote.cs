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
        /// Link to vote.
        /// </summary>
        public string VoteLink
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
        
    }
}
