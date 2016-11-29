﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class EventViewModel : IValidatableObject
    {
        public int EventId { get; set; }

        [Required]
        [Display(Name = "Event title")]
        public string EventName { get; set; }

        [Display(Name = "Event description")]
        public string EventDescription { get; set; }

        public string EventLink { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();

        public string CurrentPlaceFoursquareId { get; set; }

        [Display(Name = "Event start")]
        public DateTime CurrentTime { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(EventName))
            {
                yield return new ValidationResult("No event title was added.");
            }
        }
    }
}
