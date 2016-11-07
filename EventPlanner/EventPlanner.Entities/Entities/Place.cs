namespace EventPlanner.Entities
{
    /// <summary>
    /// Represents FourSquare location.
    /// </summary>
    public class Place
    {
        /// <summary>
        /// Primary key of place.
        /// </summary>
        public int PlaceId { get; set; }

        /// <summary>
        /// Display name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Link to the FourSquare page of location.
        /// </summary>
        public string FourSquareLink { get; set; }

        /// <summary>
        /// Event to which this belongs to.
        /// </summary>
        public Event Event { get; set; }
    }
}
