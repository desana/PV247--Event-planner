namespace EventPlanner.Services.DataTransferModels.Models
{
    public class PlaceTransferModel
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
        public EventTransferModel Event { get; set; }
    }
}