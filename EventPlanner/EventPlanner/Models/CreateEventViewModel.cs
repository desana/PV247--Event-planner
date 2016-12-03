using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class CreateEventViewModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Title")]
        public string EventName { get; set; }

        [Display(Name = "Description")]
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
