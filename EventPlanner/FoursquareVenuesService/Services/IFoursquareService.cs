using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoursquareVenuesService.Entities;

namespace FoursquareVenuesService.Services
{
    public interface IFoursquareService
    {
        /// <summary>
        /// Gets list of venues that match the given parameters.
        /// </summary>
        /// <param name="query">Search with this query.</param>
        /// <param name="city">Search venues near this city.</param>
        /// <param name="numberOfResults">Maximum number of results.</param>
        /// <returns>List of venues that match the given parameters.</returns>
        Task<IEnumerable<Venue>> SearchVenuesAsync(string query, string city, int numberOfResults);

        /// <summary>
        /// Gets venue with given id.
        /// </summary>
        /// <param name="id">Id of venue.</param>
        /// <returns>Venue with given id.</returns>
        Task<Venue> GetVenueAsync(string id);

        /// <summary>
        /// Gets url of first photo of venue
        /// </summary>
        Task<string> GetVenuePhotoUrlAsync(string venueId);
    }
}
