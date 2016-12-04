using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class AddPlacesViewModel
    {
        public int EventId { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();

        [Remote("IsCurrentPlaceUnique", "EventController")]
        public string CurrentPlaceFoursquareId { get; set; }

        [Display(Name = "Event start")]
        [Remote("IsCurrentTimeUnique", "EventController")]
        public string CurrentTime { get; set; }

        public string PlaceErrorMessage { get; set; }
    }
}
