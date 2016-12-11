using System;

namespace EventPlanner.DTO.Vote
{
    public class Vote
    {
        /// <summary>
        /// Primary key of the vote.
        /// </summary>
        public Guid VoteId { get; set; }

        /// <summary>
        /// Target time at place.
        /// </summary>
        public int TimeAtPlaceId { get; set; }

        /// <summary>
        /// Number of votes for this record.
        /// </summary>
        public VoteValueEnum Value { get; set; }
    }
}
