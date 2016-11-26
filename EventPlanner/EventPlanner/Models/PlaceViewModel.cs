namespace EventPlanner.Models
{
    public class PlaceViewModel
    {
        public int PlaceId { get; set; }
        
        public string Name { get; set; }
        
        public string FourSquareLink { get; set; }

        public int EventId { get; set; }

        public string FourSquareId { get; set; }
    }
}
