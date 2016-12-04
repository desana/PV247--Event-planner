using EventPlanner.DTO.Event;
using FoursquareVenuesService.Entities;

namespace EventPlanner.Models.Vote
{
    public class VotePlaceViewModel
    {
        public Place Place { get; set; }

        public string PlacePhotoUrl { get; set; }

        public Location Location { get; set; }
    }
}