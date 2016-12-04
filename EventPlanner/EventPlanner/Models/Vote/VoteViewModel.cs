using System.Collections.Generic;
using EventPlanner.DTO.Vote;

namespace EventPlanner.Models.Vote
{
    public class VoteViewModel
    {
        public int EventId { get; set; }

        public string EventName { get; set; }

        public List<VotePlaceViewModel> Places { get; set; }

        public VoteSession VoteSession { get; set; }

        public VoteViewModel()
        {
            Places = new List<VotePlaceViewModel>();
        }
    }
}