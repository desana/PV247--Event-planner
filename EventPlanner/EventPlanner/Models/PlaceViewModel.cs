namespace EventPlanner.Models
{
    public class PlaceViewModel
    {
        public int PlaceId { get; set; }
        
        public string Name { get; set; }
        
        public string FourSquareLink { get; set; }

        public EventViewModel Event { get; set; }
    }
}
