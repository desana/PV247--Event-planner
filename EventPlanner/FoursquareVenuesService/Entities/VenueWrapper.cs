namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Wraps data from request on venues.
    /// </summary>
    public class VenueWrapper
    {
        /// <summary>
        /// Data from venue request.
        /// </summary>
        public VenueResponse Response { get; set; }
    }
}
