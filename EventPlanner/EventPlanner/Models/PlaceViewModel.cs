namespace EventPlanner.Models
{
    public class PlaceViewModel
    {
        public int PlaceId { get; set; }
        
        public string Name { get; set; }
        
        public string FourSquareLink { get; set; }

        public string FourSquareId { get; internal set; }
    }
}
