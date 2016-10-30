using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventPlanner.Models
{
    public class CreateEventModel : IValidatableObject
    {
        [Required]
        [Display(Name = "Event title")]
        public string Title { get; set; }

        [Display(Name = "Event description")]
        public string Description { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(Title))
            {
                yield return new ValidationResult("No event title was added.");
            }
        }
    }
}
