using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanner.Models.Event
{
    public class AddPlacesViewModel
    {
        public Guid EventId { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();

        [Remote("IsCurrentPlaceUnique", "EventController")]
        public string CurrentPlaceFoursquareId { get; set; }

        [Display(Name = "Time:")]
        [Remote("IsCurrentTimeUnique", "EventController")]
        public string CurrentTime { get; set; }

        public string PlaceErrorMessage { get; set; }

        public string TimeErrorMessage { get; set; }
    }
}
