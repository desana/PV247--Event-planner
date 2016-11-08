using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoursquareVenuesService.Entities
{
    /// <summary>
    /// Representation of location from Foursquare venue (https://developer.foursquare.com/docs/responses/venue).
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Street address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// City.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// State.
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Postal code.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Country.
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Longtitude.
        /// </summary>
        public double Lng { get; set; }
    }
}
