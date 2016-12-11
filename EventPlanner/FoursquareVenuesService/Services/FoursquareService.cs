using System.Collections.Generic;
using System.Threading.Tasks;
using FoursquareVenuesService.Entities;
using Microsoft.Extensions.Options;

namespace FoursquareVenuesService.Services
{
    public class FoursquareService : IFoursquareService
    {
        private const string FUrl = "https://api.foursquare.com/v2/venues";
        private const string FVersion = "&v=20161108";

        private readonly string _authQueryString;

        public FoursquareService(IOptions<FoursquareOptions> foursquareOptions)
            : this(foursquareOptions.Value)
        {
        }

        public FoursquareService(FoursquareOptions foursquareOptions)
        {
            _authQueryString = $"client_id={foursquareOptions.ClientId}&client_secret={foursquareOptions.ClientSecret}{FVersion}";
        }

        public async Task<IEnumerable<Venue>> SearchVenuesAsync(string query, string city, int numberOfResults)
        {
            var uri = string.Format($"{FUrl}/search?query={query}&near={city}&limit={numberOfResults}&{_authQueryString}");
            var response = await HttpHelper.GetResult<VenueSearchWrapper>(uri);
            return response.Response.Venues;
        }

        public async Task<Venue> GetVenueAsync(string id)
        {
            var uri = $"{FUrl}/{id}?{_authQueryString}";          
            var response = await HttpHelper.GetResult<VenueWrapper>(uri);
            return response.Response.Venue;
        }

        /// <summary>
        /// Gets url of first photo of venue
        /// </summary>
        public async Task<string> GetVenuePhotoUrlAsync(string venueId, string size)
        {
            var uri = string.Format($"{FUrl}/{venueId}/photos?limit=1&{_authQueryString}");
            var responseType = new {response = new {photos = new {count = 0, items = new Photo[0]}}};
            var result = await HttpHelper.GetResult(uri, responseType);

            if (result?.response?.photos?.count == 1)
            {
                return $"{result.response.photos.items[0].Prefix}{size}{result.response.photos.items[0].Suffix}";
            }

            return null;
        }
    }
}
