using System;
using System.Collections.Generic;

namespace EventPlanner.Models
{
    public class EventViewModel
    {
        public string EventId { get; set; }
        
        public string EventName { get; set; }
        
        public string EventDescription { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();
    }
}
