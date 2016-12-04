
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class AddPlacesViewModel : IValidatableObject
    {
        public int EventId { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();

        [Remote("IsCurrentPlaceUnique", "EventController")]
        public string CurrentPlaceFoursquareId { get; set; }

        [Display(Name = "Event start")]
        [Remote("IsCurrentTimeUnique", "EventController")]
        public string CurrentTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(CurrentPlaceFoursquareId))
            {
                yield return new ValidationResult("No event title was added.");
            }
            else if (CurrentTime == null)
            {
                
                // check if event does not contain this fsId
            }

            if (CurrentTime != null)
            {
                // check opening hours and if time is already there
            }
        }
    }
}
