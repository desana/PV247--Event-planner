namespace EventPlanner.Services.DataTransferModels
{
    public class Event
    {
        public string EventName { get; set; }

        public string EventDescription { get; set; }

        public string EventLink { get; set; }

        public void GenerateUniqueLink()
        {
            EventLink = "ThisIsReallySoUniqueLink/ItIsNotEvenPossible/ItIsLike/Impossible";
        }
    }
}

