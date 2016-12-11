using System;
using System.Collections.Generic;

namespace EventPlanner.DTO.Vote
{
    public class VoteSession
    {
        /// <summary>
        /// Primary key of the session.
        /// </summary>
        public Guid VoteSessionId { get; set; }

        /// <summary>
        /// Id of the event to vote in.
        /// </summary>
        public Guid EventId { get; set; }

        /// <summary>
        /// Name of the voter.
        /// </summary>
        public string VoterName { get; set; }

        /// <summary>
        /// Time when the session was last modified.
        /// </summary>
        public DateTime LastModified { get; set; }

        /// <summary>
        /// Collection of votes for this session.
        /// </summary>
        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
