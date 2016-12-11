namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Representation of Foursquare venue (https://developer.foursquare.com/docs/responses/venue).
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Id of venue.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name oof venue.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Location of venue.
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// Url of venue's website.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Opening hours.
        /// </summary>
        public Hours Hours { get; set; }

        /// <summary>
        /// Photos url.
        /// </summary>
        public string PhotoUrl { get; set; }

        /// <summary>
        /// Text.
        /// </summary>
        public string Text { get; set; }
    }
}
