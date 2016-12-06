using System;
using System.Collections.Generic;

namespace EventPlanner.DTO.Vote
{
    public class VoteSession
    {
        public Guid VoteSessionId { get; set; }

        public Guid EventId { get; set; }

        public string VoterName { get; set; }

        public DateTime LastModified { get; set; }

        public virtual ICollection<Vote> Votes { get; set; } = new List<Vote>();
    }
}
