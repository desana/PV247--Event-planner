namespace EventPlanner.Models
{
    public class VoteViewModel
    {
        public int VoteId { get; set; }
        
        public TimeAtPlaceViewModel TimeAtPlace { get; set; }

        public int Votes { get; set; }
        
        public EventViewModel Event { get; set; }
    }
}