using System;

namespace EventPlanner.Models
{
    public class TimeAtPlaceViewModel
    {
        public int Id { get; set; }
        
        public PlaceViewModel Place { get; set; }
        
        public DateTime Time { get; set; }
        
        public int EventId { get; set; }

        public int Votes { get; set; }
    }
}
