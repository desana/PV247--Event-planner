using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FoursquareVenuesService.Entities;

namespace FoursquareVenuesService.Services
{
    public class FoursquareService : IFoursquareService
    {
        private const string F_URL = "https://api.foursquare.com/v2/venues/";
        private const string F_VERSION = "&v=20161108";

        private static HttpClient _client;
        private readonly string _clientId;
        private readonly string _clientSecret;

        public FoursquareService()
        {
            _client = new HttpClient();
            // TODO add App.config file with those keys, do not share it via github
            _clientId = ConfigurationManager.AppSettings["clientId"];
            _clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        }

        public async Task<IEnumerable<Venue>> SearchVenuesAsync(string query, string city, int numberOfResults)
        {            
            throw new NotImplementedException();
        }

        public async Task<Venue> GetVenueAsync(int id)
        {
            var uri = F_URL + id + "?" + "client_id=" + _clientId + "&client_secret=" + _clientSecret;          
            return await HttpHelper<Venue>.GetResult(_client, uri);
        }
    }
}
