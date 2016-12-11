using System.Collections.Generic;

namespace EventPlanner.Models.Event
{
    public class ChartModel
    {
        public string EventName { get; set; }

        public ICollection<string> Categories { get; set; } = new List<string>();

        public ICollection<int> Data { get; set; } = new List<int>();
    }
}