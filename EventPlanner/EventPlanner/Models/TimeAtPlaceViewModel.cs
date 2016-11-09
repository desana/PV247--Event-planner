using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventPlanner.Models
{
    public class TimeAtPlaceViewModel
    {
        public int TimeAtPlaceId { get; set; }
        
        public PlaceViewModel Place { get; set; }
        
        public List<DateTime> Time { get; set; }
        
        public EventViewModel Event { get; set; }
    }
}
