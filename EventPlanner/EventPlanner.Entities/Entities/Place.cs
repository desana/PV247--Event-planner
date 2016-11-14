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
        public int Id { get; set; }

        /// <summary>
        /// Display name of the location.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Id of the FourSquare location.
        /// </summary>
        public string FourSquareId { get; set; }

        /// <summary>
        /// Link to FourSquare location.
        /// </summary>
        public string FourSquareLink { get; set; }
    }
}
