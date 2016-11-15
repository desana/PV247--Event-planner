using System;

namespace EventPlanner.Models
{
    public class TimeAtPlaceViewModel
    {
        public int Id { get; set; }
        
        public PlaceViewModel Place { get; set; }
        
        public DateTime Time { get; set; }
        
        public EventViewModel Event { get; set; }

        public int Votes { get; set; }
    }
}
