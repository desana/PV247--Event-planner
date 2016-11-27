using System;
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

        [Display(Name = "City")]
        public string CurrentCity { get; set; }

        [Display(Name = "Place")]
        public string CurrentPlace { get; set; }

        [Display(Name = "Event start")]
        public DateTime CurrentTime { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(EventName))
            {
                yield return new ValidationResult("No event title was added.");
            }
        }
        
        //public Dictionary<PlaceViewModel, List<DateTime>> GetTimesWithCoupledPlaces()
        //{
        //    Dictionary<PlaceViewModel, List<DateTime>> results = new Dictionary<PlaceViewModel, List<DateTime>>  ();

            
        //    foreach (var record in TimesAtPlaces)
        //    {
        //        if (results[record.Place] == null)
        //        {
        //            results[record.Place] = new List<DateTime>();
        //        }
                
        //        results[record.Place].Add(record.Time);
        //    }

        //    return results;
        //}

    }
}
