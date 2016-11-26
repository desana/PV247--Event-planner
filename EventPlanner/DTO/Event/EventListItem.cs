namespace EventPlanner.DTO.Event
{
    public class EventListItem
    {
        /// <summary>
        /// Primary key of the event.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the event.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of the event.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Link to vote.
        /// </summary>
        public string Link { get; set; }
    }
}
