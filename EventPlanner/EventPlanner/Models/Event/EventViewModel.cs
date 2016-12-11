using System;

namespace EventPlanner.Models.Event
{
    public class EventViewModel
    {
        public Guid EventId { get; set; }
        
        public string EventName { get; set; }
        
        public string EventDescription { get; set; }
    }
}
