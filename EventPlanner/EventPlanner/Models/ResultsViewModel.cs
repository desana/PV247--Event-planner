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

        public ICollection<VoteSession> VoteSessions { get; set; } = new List<VoteSession>();

        public ICollection<TimeAtPlaceViewModel> TimesAtPlaces { get; set; } = new List<TimeAtPlaceViewModel>();
    }
}
