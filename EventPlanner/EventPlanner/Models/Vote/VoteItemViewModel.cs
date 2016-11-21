using System;
using System.Collections.Generic;
using EventPlanner.Services.DataTransferModels.Models;

namespace EventPlanner.Models.Vote
{
    public class VoteItemViewModel
    {
        public DateTime Time { get; set; }

        public PlaceTransferModel Place { get; set; }

        public string PlacePhotoUrl { get; set; }
    }
}