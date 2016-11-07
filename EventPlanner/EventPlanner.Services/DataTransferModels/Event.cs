namespace EventPlanner.Services.DataTransferModels
{
    public class Event
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public string EventId { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string EventDescription { get; set; }

        /// <summary>
        /// Link to vote.
        /// </summary>
        public string EventLink { get; set; }
    }
}

