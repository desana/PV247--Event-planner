using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class EventViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Event title")]
        public string EventName { get; set; }

        [Display(Name = "Event description")]
        public string EventDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(EventName))
            {
                yield return new ValidationResult("No event title was added.");
            }
        }
    }
}
