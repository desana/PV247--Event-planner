using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventPlanner.DTO.Vote;

namespace EventPlanner.Models
{
    public class ResultsViewModel
    {
        public ChartModel ChartData { get; set; }

        public string EventLink { get; set; }

        public ICollection<VoteSession> VoteSessions { get; set; } = new List<VoteSession>();

        public IDictionary<int, string> TimesAtPlaces { get; set; } = new Dictionary<int, string>();
    }
}
