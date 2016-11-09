namespace FoursquareVenuesService.Entities
{

    /// <summary>
    /// Wraps data from request on venues/search.
    /// </summary>
    public class VenueSearchWrapper
    {
        /// <summary>
        /// Data from venues/search request.
        /// </summary>
        public VenueSearchResponse Response { get; set; }
    }
}