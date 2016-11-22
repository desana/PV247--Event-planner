using System.Collections.Generic;

namespace EventPlanner.Models.Vote
{
    public class VoteCollectionViewModel
    {
        public string EventName { get; set; }

        public List<VoteItemViewModel> VoteItems { get; set; }

        public VoteCollectionViewModel()
        {
            VoteItems = new List<VoteItemViewModel>();
        }
    }
}