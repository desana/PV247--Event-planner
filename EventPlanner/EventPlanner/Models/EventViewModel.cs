using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EventPlanner.Entities;

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

        public ICollection<VoteViewModel> Votes { get; set; } = new List<VoteViewModel>();

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();

        public ICollection<TimeAtPlaceViewModel> TimesAtPlaces { get; set; } = new List<TimeAtPlaceViewModel>();


        public int CurrentPlaceFoursquareId { get; set; }
        public string CurrentCity { get; set; }
        public string CurrentPlace { get; set; }
        public DateTime CurrentTime { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (String.IsNullOrWhiteSpace(EventName))
            {
                yield return new ValidationResult("No event title was added.");
            }
        }




        public Dictionary<PlaceViewModel, List<DateTime>> GetTimesWithCoupledPlaces()
        {
            Dictionary<PlaceViewModel, List<DateTime>> results = new Dictionary<PlaceViewModel, List<DateTime>>  ();

            
            foreach (var record in TimesAtPlaces)
            {
                if (results[record.Place] == null)
                {
                    results[record.Place] = new List<DateTime>();
                }
                
                results[record.Place].Add(record.Time);
            }

            return results;
        }

    }
}
