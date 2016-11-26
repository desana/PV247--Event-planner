using System;
using EventPlanner.DTO.Event;

namespace EventPlanner.DTO.Vote
{
    public class Vote
    {
        public Guid VoteId { get; set; }

        public int TimeAtPlaceId { get; set; }

        public VoteValueEnum Value { get; set; }
    }
}
