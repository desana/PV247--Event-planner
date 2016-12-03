using System.Collections.Generic;

namespace EventPlanner.Models
{
    public class EventViewModel
    {
        public int EventId { get; set; }
        
        public string EventName { get; set; }
        
        public string EventDescription { get; set; }

        public string EventLink { get; set; }

        public ICollection<PlaceViewModel> Places { get; set; } = new List<PlaceViewModel>();
    }
}
